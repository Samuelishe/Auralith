using Auralith.Playback;
using HanumanInstitute.LibMpv;
using System.Globalization;
using System.IO;

namespace Auralith.Playback.Mpv;

public sealed class MpvPlaybackSession : IPlaybackSession
{
    private const double SeekConfirmationToleranceSeconds = 2;
    private readonly MpvContext _mpv;

    public MpvPlaybackSession(MpvContext mpv)
    {
        _mpv = mpv;
    }

    public string? CurrentMediaPath { get; private set; }
    public string? LastDiagnosticMessage { get; private set; }
    public string? DebugSnapshot { get; private set; }

    public bool IsPaused
    {
        get => ReadProperty("pause", false);
        set => _mpv.SetProperty("pause", value);
    }

    public TimeSpan Position => TimeSpan.FromSeconds(SanitizeSeconds(ReadProperty("time-pos", 0d)));

    public TimeSpan Duration => TimeSpan.FromSeconds(SanitizeSeconds(ReadProperty("duration", 0d)));

    public bool Seekable => ReadProperty("seekable", false);

    public double Volume
    {
        get => ReadProperty("volume", 70d);
        set => _mpv.SetProperty("volume", PlaybackConstraints.ClampVolume(value));
    }

    public void Open(string path)
    {
        Report($"Opening media: {path}");
        CurrentMediaPath = path;
        _mpv.LoadFile(path).Invoke();
        IsPaused = false;
        Report("Media open command sent");
        RefreshDebugSnapshot();
    }

    public void Stop()
    {
        Report("Stop requested");
        _mpv.Stop().Invoke();
        CurrentMediaPath = null;
        RefreshDebugSnapshot();
    }

    public void Seek(TimeSpan position)
    {
        var requested = PlaybackConstraints.ClampPosition(position);
        var before = Position;
        var duration = Duration;
        var mediaName = CurrentMediaPath is null ? "<none>" : Path.GetFileName(CurrentMediaPath);
        var wasPaused = IsPaused;
        var seekable = Seekable;
        Report($"Seek requested: target={requested.TotalSeconds:0.###}s; duration={duration.TotalSeconds:0.###}s; before={before.TotalSeconds:0.###}s; media={mediaName}; paused={wasPaused}; seekable={seekable}");
        RefreshDebugSnapshot(requested, "not sent yet", null);

        if (duration <= TimeSpan.Zero)
        {
            Report("Seek skipped: duration is not available yet");
            return;
        }

        if (!seekable)
        {
            Report("Seek skipped: mpv reports seekable=false");
            return;
        }

        _ = SeekWithFallbacksAsync(requested);
    }

    public void SeekRelative(TimeSpan offset)
    {
        var target = Position + offset;
        Report($"Relative seek requested: current={Position.TotalSeconds:0.###}s; offset={offset.TotalSeconds:0.###}s; target={target.TotalSeconds:0.###}s");
        Seek(target);
    }

    private T ReadProperty<T>(string name, T fallback)
    {
        try
        {
            return _mpv.GetProperty<T>(name) ?? fallback;
        }
        catch
        {
            return fallback;
        }
    }

    private async Task SeekWithFallbacksAsync(TimeSpan requested)
    {
        var seconds = requested.TotalSeconds.ToString("0.###", CultureInfo.InvariantCulture);
        var attempts = new (string Name, Action Send)[]
        {
            ("RunCommand split flags: seek target absolute exact", () => _mpv.RunCommand(CreateCommandOptions(), "seek", seconds, "absolute", "exact")),
            ("RunCommand combined flags: seek target absolute+exact", () => _mpv.RunCommand(CreateCommandOptions(), "seek", seconds, "absolute+exact")),
            ("RunCommand absolute only: seek target absolute", () => _mpv.RunCommand(CreateCommandOptions(), "seek", seconds, "absolute")),
            ("RunCommandString: seek target absolute exact", () => CheckCommandStringResult(_mpv.RunCommandString(FormattableString.Invariant($"seek {requested.TotalSeconds:0.###} absolute exact")))),
            ("MpvContext.Seek Absolute", () => _mpv.Seek(requested.TotalSeconds, SeekOption.Absolute).Invoke()),
            ("SetPropertyDouble time-pos", () => _mpv.SetPropertyDouble("time-pos", requested.TotalSeconds))
        };

        Exception? lastException = null;
        foreach (var attempt in attempts)
        {
            try
            {
                Report($"Seek attempt: {attempt.Name}");
                attempt.Send();
                Report($"Seek attempt sent: {attempt.Name}");
                await ReportPositionAfterDelayAsync(requested, 100, attempt.Name);
                await ReportPositionAfterDelayAsync(requested, 400, attempt.Name);
                await ReportPositionAfterDelayAsync(requested, 500, attempt.Name);

                var delta = Math.Abs((Position - requested).TotalSeconds);
                if (delta <= SeekConfirmationToleranceSeconds)
                {
                    Report($"Seek confirmed by {attempt.Name}; delta={delta:0.###}s");
                    RefreshDebugSnapshot(requested, attempt.Name, null);
                    return;
                }

                Report($"Seek attempt did not move near target: {attempt.Name}; delta={delta:0.###}s");
            }
            catch (Exception ex)
            {
                lastException = ex;
                Report($"Seek attempt failed: {attempt.Name}; {ex.GetType().Name}: {ex.Message}");
                RefreshDebugSnapshot(requested, attempt.Name, ex);
            }
        }

        Report(lastException is null
            ? "Seek failed: all command variants completed without position reaching target"
            : $"Seek failed: all command variants failed or did not move position; last={lastException.GetType().Name}: {lastException.Message}");
        RefreshDebugSnapshot(requested, "all variants exhausted", lastException);
    }

    private async Task ReportPositionAfterDelayAsync(TimeSpan requested, int delayMilliseconds, string commandPath)
    {
        await Task.Delay(delayMilliseconds);
        var position = Position;
        var delta = Math.Abs((position - requested).TotalSeconds);
        var seeking = ReadProperty("seeking", false);
        Report($"Seek follow-up +{delayMilliseconds}ms: position={position.TotalSeconds:0.###}s; target={requested.TotalSeconds:0.###}s; delta={delta:0.###}s; paused={IsPaused}; seeking={seeking}; path={commandPath}");
        RefreshDebugSnapshot(requested, commandPath, null);
    }

    private static MpvCommandOptions CreateCommandOptions()
    {
        return new MpvCommandOptions
        {
            Sync = true,
            ThrowOnError = true,
            NoOsd = true
        };
    }

    private static void CheckCommandStringResult(int result)
    {
        if (result < 0)
        {
            throw new InvalidOperationException($"RunCommandString returned mpv error code {result}");
        }
    }

    private void RefreshDebugSnapshot(TimeSpan? target = null, string? commandPath = null, Exception? exception = null)
    {
        var mediaName = CurrentMediaPath is null ? "<none>" : Path.GetFileName(CurrentMediaPath);
        DebugSnapshot =
            $"Media: {mediaName}\n" +
            $"Duration: {Duration.TotalSeconds:0.###}s\n" +
            $"Position: {Position.TotalSeconds:0.###}s\n" +
            $"Seekable: {Seekable}\n" +
            $"Paused: {IsPaused}\n" +
            $"Last seek target: {(target is null ? "<none>" : $"{target.Value.TotalSeconds:0.###}s")}\n" +
            $"Last seek path: {commandPath ?? "<none>"}\n" +
            $"Last seek error: {(exception is null ? "<none>" : $"{exception.GetType().Name}: {exception.Message}")}\n" +
            $"Last message: {LastDiagnosticMessage ?? "<none>"}";
    }

    private static double SanitizeSeconds(double value)
    {
        return double.IsFinite(value) ? Math.Max(0, value) : 0;
    }

    private void Report(string message)
    {
        LastDiagnosticMessage = message;
        var fullMessage = $"[Auralith.Playback.Mpv] {message}";
        Console.WriteLine(fullMessage);
        System.Diagnostics.Debug.WriteLine(fullMessage);
    }
}
