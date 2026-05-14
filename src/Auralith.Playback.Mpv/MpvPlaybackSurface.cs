using System.Runtime.InteropServices;
using Auralith.Playback;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
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
        if (!CanLoadMpvNative())
        {
            Fail("libmpv native library failed to load");
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

    private static bool CanLoadMpvNative()
    {
        if (!NativeLibrary.TryLoad("libmpv.2", out var handle))
        {
            return false;
        }

        NativeLibrary.Free(handle);
        return true;
    }

    private void Fail(string message)
    {
        Content = new TextBlock
        {
            Text = "libmpv native library was not found. Copy a compatible libmpv build next to the app output and restart.",
            Foreground = Brushes.White,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Avalonia.Thickness(24)
        };

        Failed?.Invoke(this, new PlaybackSurfaceFailedEventArgs(message));
    }
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
