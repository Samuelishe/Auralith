using System.Runtime.InteropServices;
using Auralith.Playback;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using HanumanInstitute.LibMpv.Core;
using HanumanInstitute.LibMpv.Avalonia;

namespace Auralith.Playback.Mpv;

public sealed class MpvPlaybackSurface : ContentControl
{
    public event EventHandler<PlaybackSurfaceReadyEventArgs>? Ready;
    public event EventHandler<PlaybackSurfaceFailedEventArgs>? Failed;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.UIThread.Post(InitializeSurface);
    }

    private void InitializeSurface()
    {
        var nativeStatus = NativeMpvRuntime.Configure();
        if (!nativeStatus.IsAvailable)
        {
            Fail(nativeStatus.Message);
            return;
        }

        var view = new MpvView();
        view.ViewInitialized += (_, _) =>
        {
            if (view.MpvContext is null)
            {
                Fail("libmpv context was not initialized");
                return;
            }

            Ready?.Invoke(this, new PlaybackSurfaceReadyEventArgs(new MpvPlaybackSession(view.MpvContext)));
        };

        Content = view;
    }

    private void Fail(string message)
    {
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
