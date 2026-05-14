using System;
using System.IO;
using System.Threading.Tasks;
using Auralith.Playback;
using Auralith.Playback.Mpv;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;

namespace Auralith.App;

public sealed partial class MainWindow : Window
{
    private readonly DispatcherTimer _positionTimer;
    private readonly DispatcherTimer _overlayIdleTimer;
    private readonly DispatcherTimer _contextNoticeTimer;
    private IPlaybackSession? _playback;
    private bool _isSeeking;
    private bool _overlayPinned;
    private bool _suppressVolumeChange;

    public MainWindow()
    {
        InitializeComponent();

        _positionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        _positionTimer.Tick += PositionTimer_Tick;

        _overlayIdleTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2.5) };
        _overlayIdleTimer.Tick += OverlayIdleTimer_Tick;

        _contextNoticeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.2) };
        _contextNoticeTimer.Tick += ContextNoticeTimer_Tick;

        ShowOverlay();
    }

    private void PlaybackSurface_Ready(object? sender, PlaybackSurfaceReadyEventArgs e)
    {
        _playback = e.PlaybackSession;
        _playback.Volume = VolumeSlider.Value;
        _positionTimer.Start();
    }

    private void PlaybackSurface_Failed(object? sender, PlaybackSurfaceFailedEventArgs e)
    {
        MediaPathText.Text = e.Message;
    }

    private async void OpenButton_Click(object? sender, RoutedEventArgs e)
    {
        await OpenMediaAsync();
    }

    private async Task OpenMediaAsync()
    {
        if (_playback is null)
        {
            MediaPathText.Text = "Playback surface is not ready yet";
            return;
        }

        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open media file",
            AllowMultiple = false
        });

        if (files.Count == 0)
        {
            return;
        }

        var path = files[0].TryGetLocalPath();
        if (string.IsNullOrWhiteSpace(path))
        {
            MediaPathText.Text = "Only local files are supported in this spike";
            return;
        }

        _playback.Open(path);
        MediaPathText.Text = Path.GetFileName(path);
        ShowOverlay();
    }

    private void PlayPauseButton_Click(object? sender, RoutedEventArgs e)
    {
        TogglePlayPause();
    }

    private void StopButton_Click(object? sender, RoutedEventArgs e)
    {
        _playback?.Stop();
        UpdatePlaybackState();
        ShowOverlay();
    }

    private void VideoSurface_PointerMoved(object? sender, PointerEventArgs e)
    {
        ShowOverlay();
    }

    private void VideoSurface_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetCurrentPoint(VideoSurface);
        if (point.Properties.IsRightButtonPressed)
        {
            ShowContextMenuPlaceholder();
            e.Handled = true;
            return;
        }

        if (point.Properties.IsLeftButtonPressed)
        {
            TogglePlayPause();
            e.Handled = true;
        }
    }

    private void VideoSurface_DoubleTapped(object? sender, TappedEventArgs e)
    {
        ToggleFullscreen();
        e.Handled = true;
    }

    private void TimelineSlider_PointerEntered(object? sender, PointerEventArgs e)
    {
        _overlayPinned = true;
        TimelineSlider.Height = 32;
        ShowOverlay();
    }

    private void TimelineSlider_PointerExited(object? sender, PointerEventArgs e)
    {
        _overlayPinned = false;
        TimelineSlider.Height = double.NaN;
        RestartOverlayIdleTimer();
    }

    private void TimelineSlider_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _isSeeking = true;
        ShowOverlay();
    }

    private void TimelineSlider_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        SeekToSliderValue();
        _isSeeking = false;
        RestartOverlayIdleTimer();
    }

    private void TimelineSlider_PropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == Slider.ValueProperty && _isSeeking)
        {
            UpdateTimeText(TimeSpan.FromSeconds(TimelineSlider.Value), _playback?.Duration ?? TimeSpan.Zero);
        }
    }

    private void VolumeSlider_PropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (_suppressVolumeChange || e.Property != Slider.ValueProperty || _playback is null)
        {
            return;
        }

        _playback.Volume = VolumeSlider.Value;
        ShowOverlay();
    }

    private void Overlay_PointerEntered(object? sender, PointerEventArgs e)
    {
        _overlayPinned = true;
        ShowOverlay();
    }

    private void Overlay_PointerExited(object? sender, PointerEventArgs e)
    {
        _overlayPinned = false;
        RestartOverlayIdleTimer();
    }

    private void PositionTimer_Tick(object? sender, EventArgs e)
    {
        UpdatePlaybackState();
    }

    private void OverlayIdleTimer_Tick(object? sender, EventArgs e)
    {
        if (!_overlayPinned)
        {
            Overlay.IsVisible = false;
            _overlayIdleTimer.Stop();
        }
    }

    private void ContextNoticeTimer_Tick(object? sender, EventArgs e)
    {
        ContextMenuNotice.IsVisible = false;
        _contextNoticeTimer.Stop();
    }

    private void TogglePlayPause()
    {
        if (_playback is null)
        {
            return;
        }

        _playback.IsPaused = !_playback.IsPaused;
        UpdatePlaybackState();
        ShowOverlay();
    }

    private void ToggleFullscreen()
    {
        WindowState = WindowState == WindowState.FullScreen
            ? WindowState.Normal
            : WindowState.FullScreen;
        ShowOverlay();
    }

    private void SeekToSliderValue()
    {
        if (_playback is null)
        {
            return;
        }

        _playback.Seek(TimeSpan.FromSeconds(TimelineSlider.Value));
    }

    private void UpdatePlaybackState()
    {
        if (_playback is null)
        {
            return;
        }

        var duration = _playback.Duration;
        var position = _playback.Position;

        if (!_isSeeking)
        {
            TimelineSlider.Maximum = Math.Max(duration.TotalSeconds, 1);
            TimelineSlider.Value = Math.Clamp(position.TotalSeconds, 0, TimelineSlider.Maximum);
        }

        _suppressVolumeChange = true;
        VolumeSlider.Value = Math.Clamp(_playback.Volume, 0, 100);
        _suppressVolumeChange = false;

        PlayPauseButton.Content = _playback.IsPaused ? "Play" : "Pause";
        UpdateTimeText(position, duration);
    }

    private void UpdateTimeText(TimeSpan position, TimeSpan duration)
    {
        TimeText.Text = $"{FormatTime(position)} / {FormatTime(duration)}";
    }

    private static string FormatTime(TimeSpan value)
    {
        if (value.TotalHours >= 1)
        {
            return value.ToString(@"h\:mm\:ss");
        }

        return value.ToString(@"mm\:ss");
    }

    private void ShowOverlay()
    {
        Overlay.IsVisible = true;
        RestartOverlayIdleTimer();
    }

    private void RestartOverlayIdleTimer()
    {
        _overlayIdleTimer.Stop();
        if (!_overlayPinned)
        {
            _overlayIdleTimer.Start();
        }
    }

    private void ShowContextMenuPlaceholder()
    {
        ContextMenuNotice.IsVisible = true;
        _contextNoticeTimer.Stop();
        _contextNoticeTimer.Start();
        ShowOverlay();
    }
}
