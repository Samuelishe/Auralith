using Auralith.Playback;
using HanumanInstitute.LibMpv;

namespace Auralith.Playback.Mpv;

public sealed class MpvPlaybackSession : IPlaybackSession
{
    private readonly MpvContext _mpv;

    public MpvPlaybackSession(MpvContext mpv)
    {
        _mpv = mpv;
    }

    public string? CurrentMediaPath { get; private set; }

    public bool IsPaused
    {
        get => ReadProperty("pause", false);
        set => _mpv.SetProperty("pause", value);
    }

    public TimeSpan Position => TimeSpan.FromSeconds(Math.Max(0, ReadProperty("time-pos", 0d)));

    public TimeSpan Duration => TimeSpan.FromSeconds(Math.Max(0, ReadProperty("duration", 0d)));

    public double Volume
    {
        get => ReadProperty("volume", 70d);
        set => _mpv.SetProperty("volume", PlaybackConstraints.ClampVolume(value));
    }

    public void Open(string path)
    {
        CurrentMediaPath = path;
        _mpv.LoadFile(path).Invoke();
        IsPaused = false;
    }

    public void Stop()
    {
        _mpv.Stop().Invoke();
        CurrentMediaPath = null;
    }

    public void Seek(TimeSpan position)
    {
        _mpv.SetProperty("time-pos", PlaybackConstraints.ClampPosition(position).TotalSeconds);
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
}
