using System.Runtime.InteropServices;
using Auralith.Playback;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.VisualTree;
using HanumanInstitute.LibMpv.Core;
using HanumanInstitute.LibMpv.Avalonia;

namespace Auralith.Playback.Mpv;

public sealed class MpvPlaybackSurface : ContentControl
{
    private const int MaxContextProbeAttempts = 40;
    private static readonly TimeSpan ContextProbeInterval = TimeSpan.FromMilliseconds(100);
    private bool _initializationStarted;
    private bool _readyRaised;

    public event EventHandler<PlaybackSurfaceStatusChangedEventArgs>? StatusChanged;
    public event EventHandler<PlaybackSurfaceReadyEventArgs>? Ready;
    public event EventHandler<PlaybackSurfaceFailedEventArgs>? Failed;

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (_initializationStarted)
        {
            return;
        }

        _initializationStarted = true;
        ReportStatus("Initializing playback surface");
        Dispatcher.UIThread.Post(InitializeSurface);
    }

    private void InitializeSurface()
    {
        try
        {
            ReportStatus("Checking native libmpv runtime");
            var nativeStatus = NativeMpvRuntime.Configure();
            ReportStatus(nativeStatus.Message);
            if (!nativeStatus.IsAvailable)
            {
                Fail(nativeStatus.Message);
                return;
            }

            ReportStatus("Creating MpvView");
            var view = new MpvView { Renderer = VideoRenderer.OpenGl };
            view.PropertyChanged += (_, e) =>
            {
                if (e.Property == MpvView.MpvContextProperty)
                {
                    ReportStatus($"MpvContext property changed. Is null: {view.MpvContext is null}");
                    CompleteReadyIfContextAvailable(view);
                }
            };

            view.ViewInitialized += (_, _) =>
            {
                ReportStatus($"MpvView.ViewInitialized fired. MpvContext is null: {view.MpvContext is null}");
                CompleteReadyIfContextAvailable(view);
            };

            Content = view;
            ReportStatus("MpvView assigned to playback surface");
            ProbeForMpvContext(view, 1);
        }
        catch (Exception ex)
        {
            Fail($"Playback surface initialization failed: {ex.Message}");
        }
    }

    private void ProbeForMpvContext(MpvView view, int attempt)
    {
        if (CompleteReadyIfContextAvailable(view))
        {
            return;
        }

        if (attempt >= MaxContextProbeAttempts)
        {
            Fail("Playback surface did not become ready. MpvView was created, but MpvContext stayed null.");
            return;
        }

        ReportStatus($"Waiting for MpvContext ({attempt}/{MaxContextProbeAttempts})");
        DispatcherTimer.RunOnce(
            () => ProbeForMpvContext(view, attempt + 1),
            ContextProbeInterval,
            DispatcherPriority.Background);
    }

    private bool CompleteReadyIfContextAvailable(MpvView view)
    {
        if (_readyRaised || view.MpvContext is null)
        {
            return _readyRaised;
        }

        _readyRaised = true;
        ReportStatus("Playback surface ready");
        Ready?.Invoke(this, new PlaybackSurfaceReadyEventArgs(new MpvPlaybackSession(view.MpvContext)));
        return true;
    }

    private void Fail(string message)
    {
        ReportStatus($"Playback surface failed: {message}");
        Content = new TextBlock
        {
            Text = message,
            Foreground = Brushes.White,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Avalonia.Thickness(24)
        };

        Failed?.Invoke(this, new PlaybackSurfaceFailedEventArgs(message));
    }

    private void ReportStatus(string message)
    {
        var fullMessage = $"[Auralith.Playback.Mpv] {message}";
        Console.WriteLine(fullMessage);
        System.Diagnostics.Debug.WriteLine(fullMessage);
        StatusChanged?.Invoke(this, new PlaybackSurfaceStatusChangedEventArgs(message));
    }
}

public static class NativeMpvRuntime
{
    public static NativeMpvRuntimeStatus Configure()
    {
        var candidate = FindNativeRoot();
        if (candidate is null)
        {
            return new NativeMpvRuntimeStatus(false, MissingNativeMessage());
        }

        MpvApi.RootPath = candidate;
        return new NativeMpvRuntimeStatus(true, $"Using native libmpv from {candidate}");
    }

    private static string? FindNativeRoot()
    {
        var fileName = NativeFileName();
        foreach (var directory in CandidateDirectories())
        {
            if (File.Exists(Path.Combine(directory, fileName)))
            {
                return directory;
            }
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            foreach (var directory in new[] { "/usr/lib", "/usr/lib64", "/lib", "/lib64" })
            {
                if (File.Exists(Path.Combine(directory, fileName)))
                {
                    return directory;
                }
            }
        }

        return null;
    }

    private static IEnumerable<string> CandidateDirectories()
    {
        var baseDirectory = AppContext.BaseDirectory;
        yield return baseDirectory;
        yield return Path.Combine(baseDirectory, "runtimes", RuntimeIdentifier(), "native");

        var current = new DirectoryInfo(baseDirectory);
        while (current is not null)
        {
            yield return Path.Combine(current.FullName, "runtimes", RuntimeIdentifier(), "native");
            current = current.Parent;
        }
    }

    private static string NativeFileName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "libmpv-2.dll";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return "libmpv.so.2";
        }

        return "libmpv";
    }

    private static string RuntimeIdentifier()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "win-x64";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return "linux-x64";
        }

        return "native";
    }

    private static string MissingNativeMessage()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "Missing native libmpv runtime. Auralith Phase 1 expected libmpv-2.dll in runtimes/win-x64/native or next to the app output. Copy a compatible Windows libmpv/mpv runtime with companion DLLs for dev-time playback validation. Future Windows releases should bundle this runtime.";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return "Missing native libmpv runtime. Auralith Phase 1 expected libmpv.so.2 from the system library path or runtimes/linux-x64/native. Install system libmpv through the distribution package manager for dev-time playback validation.";
        }

        return "Native libmpv was not found for this platform.";
    }
}

public readonly record struct NativeMpvRuntimeStatus(bool IsAvailable, string Message);

public sealed class PlaybackSurfaceStatusChangedEventArgs : EventArgs
{
    public PlaybackSurfaceStatusChangedEventArgs(string message)
    {
        Message = message;
    }

    public string Message { get; }
}

public sealed class PlaybackSurfaceReadyEventArgs : EventArgs
{
    public PlaybackSurfaceReadyEventArgs(IPlaybackSession playbackSession)
    {
        PlaybackSession = playbackSession;
    }

    public IPlaybackSession PlaybackSession { get; }
}

public sealed class PlaybackSurfaceFailedEventArgs : EventArgs
{
    public PlaybackSurfaceFailedEventArgs(string message)
    {
        Message = message;
    }

    public string Message { get; }
}
