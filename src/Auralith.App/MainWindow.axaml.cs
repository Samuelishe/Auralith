using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auralith.Core;
using Auralith.Playback;
using Auralith.Playback.Mpv;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;

namespace Auralith.App;

public sealed partial class MainWindow : Window
{
    private static readonly TimeSpan SeekConfirmationTimeout = TimeSpan.FromSeconds(2);
    private const double SeekConfirmationToleranceSeconds = 2;
    private readonly DispatcherTimer _positionTimer;
    private readonly DispatcherTimer _contextNoticeTimer;
    private MediaOpenRequest? _pendingOpenRequest;
    private IPlaybackSession? _playback;
    private string? _playbackFailureMessage;
    private double? _pendingSeekTargetSeconds;
    private DateTimeOffset _pendingSeekDeadline;
    private DateTimeOffset _suppressPositionPollingUntil;
    private bool _isSeeking;
    private bool _suppressVolumeChange;

    public MainWindow()
        : this(null)
    {
    }

    public MainWindow(MediaOpenRequest? startupOpenRequest)
    {
        _pendingOpenRequest = startupOpenRequest;
        InitializeComponent();
        if (_pendingOpenRequest is not null)
        {
            Report($"Startup media queued: {_pendingOpenRequest.Path}");
            MediaPathText.Text = "Media queued until playback surface becomes ready";
        }

        _positionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        _positionTimer.Tick += PositionTimer_Tick;

        _contextNoticeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.2) };
        _contextNoticeTimer.Tick += ContextNoticeTimer_Tick;
    }

    private void PlaybackSurface_StatusChanged(object? sender, PlaybackSurfaceStatusChangedEventArgs e)
    {
        PlaybackStatusText.Text = e.Message;
    }

    private void PlaybackSurface_Ready(object? sender, PlaybackSurfaceReadyEventArgs e)
    {
        _playback = e.PlaybackSession;
        _playbackFailureMessage = null;
        PlaybackStatusText.Text = "Ready";
        _playback.Volume = VolumeSlider.Value;
        _positionTimer.Start();

        if (_pendingOpenRequest is not null)
        {
            Report($"Opening pending media after playback ready: {_pendingOpenRequest.Path}");
            OpenMedia(_pendingOpenRequest);
            _pendingOpenRequest = null;
        }
    }

    private void PlaybackSurface_Failed(object? sender, PlaybackSurfaceFailedEventArgs e)
    {
        _playbackFailureMessage = e.Message;
        PlaybackStatusText.Text = "Failed";
        MediaPathText.Text = e.Message;
    }

    private async void OpenButton_Click(object? sender, RoutedEventArgs e)
    {
        await OpenMediaAsync();
    }

    private async Task OpenMediaAsync()
    {
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

        if (!MediaOpenRequest.TryCreate(path, out var request, out var error))
        {
            MediaPathText.Text = error;
            return;
        }

        OpenMedia(request);
    }

    private void PlayPauseButton_Click(object? sender, RoutedEventArgs e)
    {
        TogglePlayPause();
    }

    private void FullscreenButton_Click(object? sender, RoutedEventArgs e)
    {
        ToggleFullscreen();
    }

    private void StopButton_Click(object? sender, RoutedEventArgs e)
    {
        Report("Stop button clicked");
        _playback?.Stop();
        TimelineSlider.Value = 0;
        UpdatePlaybackState();
    }

    private void DebugSeekForwardButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_playback is null)
        {
            PlaybackStatusText.Text = "Debug seek skipped: playback is not ready";
            return;
        }

        var target = _playback.Position + TimeSpan.FromSeconds(60);
        var duration = _playback.Duration;
        if (duration > TimeSpan.Zero && target > duration)
        {
            target = TimeSpan.FromSeconds(Math.Max(duration.TotalSeconds - 5, 0));
        }

        Report($"+60s debug seek clicked; current={_playback.Position.TotalSeconds:0.###}s; target={target.TotalSeconds:0.###}s; duration={duration.TotalSeconds:0.###}s");
        _pendingSeekTargetSeconds = target.TotalSeconds;
        _pendingSeekDeadline = DateTimeOffset.UtcNow.Add(SeekConfirmationTimeout);
        _suppressPositionPollingUntil = _pendingSeekDeadline;
        TimelineSlider.Value = Math.Clamp(target.TotalSeconds, 0, TimelineSlider.Maximum);
        _playback.Seek(target);
        UpdateSeekDebugText();
    }

    private void VideoSurface_PointerMoved(object? sender, PointerEventArgs e)
    {
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

    private void VideoSurface_DragOver(object? sender, DragEventArgs e)
    {
        e.DragEffects = e.DataTransfer.Contains(DataFormat.File)
            ? DragDropEffects.Copy
            : DragDropEffects.None;
        e.Handled = true;
    }

    private void VideoSurface_Drop(object? sender, DragEventArgs e)
    {
        var path = GetSingleDroppedFile(e);
        if (path is null)
        {
            MediaPathText.Text = "Drop exactly one local media file for this spike";
            e.Handled = true;
            return;
        }

        if (!MediaOpenRequest.TryCreate(path, out var request, out var error))
        {
            MediaPathText.Text = error;
            e.Handled = true;
            return;
        }

        OpenMedia(request);
        e.Handled = true;
    }

    private void TimelineSlider_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _isSeeking = true;
    }

    private void TimelineSlider_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        SeekToSliderValue();
        _isSeeking = false;
    }

    private void TimelineSlider_PointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (!_isSeeking)
        {
            return;
        }

        SeekToSliderValue();
        _isSeeking = false;
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
    }

    private void PositionTimer_Tick(object? sender, EventArgs e)
    {
        if (_pendingSeekTargetSeconds is { } target)
        {
            Report($"Position tick during pending seek: target={target:0.###}s; position={_playback?.Position.TotalSeconds:0.###}s; duration={_playback?.Duration.TotalSeconds:0.###}s; deadline={_pendingSeekDeadline:O}");
        }

        UpdatePlaybackState();
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
    }

    private void ToggleFullscreen()
    {
        var enteringFullscreen = WindowState != WindowState.FullScreen;
        WindowState = enteringFullscreen ? WindowState.FullScreen : WindowState.Normal;
        HeaderBar.IsVisible = !enteringFullscreen;
        ControlBar.Padding = enteringFullscreen ? new Thickness(12, 6) : new Thickness(14, 10);
        FullscreenButton.Content = enteringFullscreen ? "Exit fullscreen" : "Fullscreen";
    }

    private void SeekToSliderValue()
    {
        if (_playback is null)
        {
            return;
        }

        var durationSeconds = Math.Max(_playback.Duration.TotalSeconds, 0);
        if (durationSeconds <= 0)
        {
            PlaybackStatusText.Text = "Seek skipped: duration is not available";
            return;
        }

        var targetSeconds = Math.Clamp(TimelineSlider.Value, 0, durationSeconds);
        _pendingSeekTargetSeconds = targetSeconds;
        _pendingSeekDeadline = DateTimeOffset.UtcNow.Add(SeekConfirmationTimeout);
        _suppressPositionPollingUntil = _pendingSeekDeadline;
        TimelineSlider.Value = targetSeconds;
        UpdateTimeText(TimeSpan.FromSeconds(targetSeconds), _playback.Duration);

        _playback.Seek(TimeSpan.FromSeconds(targetSeconds));
        if (!string.IsNullOrWhiteSpace(_playback.LastDiagnosticMessage))
        {
            PlaybackStatusText.Text = _playback.LastDiagnosticMessage;
        }
    }

    private void OpenMedia(MediaOpenRequest? request)
    {
        if (request is null)
        {
            return;
        }

        if (_playback is null)
        {
            _pendingOpenRequest = request;
            Report($"Media queued until playback ready: {request.Path}");
            MediaPathText.Text = _playbackFailureMessage is null
                ? "Media queued until playback surface becomes ready"
                : $"Media queued, but playback surface is failed: {_playbackFailureMessage}";
            return;
        }

        try
        {
            PlaybackStatusText.Text = "Opening media";
            Report($"OpenMedia calls playback.Open: {request.Path}");
            _playback.Open(request.Path);
            MediaPathText.Text = Path.GetFileName(request.Path);
            PlaybackStatusText.Text = "Media opened";
            UpdatePlaybackState();
        }
        catch (Exception ex)
        {
            PlaybackStatusText.Text = "Open failed";
            MediaPathText.Text = $"Failed to open media: {ex.Message}";
        }
    }

    private static string? GetSingleDroppedFile(DragEventArgs e)
    {
        var files = e.DataTransfer.TryGetFiles();
        var paths = files?
            .Select(x => x.TryGetLocalPath())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Cast<string>()
            .ToArray();

        return paths is [var singlePath] ? singlePath : null;
    }
    private void UpdatePlaybackState()
    {
        if (_playback is null)
        {
            return;
        }

        var duration = _playback.Duration;
        var position = _playback.Position;
        var maximum = Math.Max(duration.TotalSeconds, 1);
        TimelineSlider.Maximum = maximum;

        if (_isSeeking)
        {
            UpdateTimeText(TimeSpan.FromSeconds(TimelineSlider.Value), duration);
        }
        else if (_pendingSeekTargetSeconds is { } targetSeconds)
        {
            var now = DateTimeOffset.UtcNow;
            var delta = Math.Abs(position.TotalSeconds - targetSeconds);
            if (delta <= SeekConfirmationToleranceSeconds)
            {
                _pendingSeekTargetSeconds = null;
                _suppressPositionPollingUntil = DateTimeOffset.MinValue;
                PlaybackStatusText.Text = "Seek confirmed";
                TimelineSlider.Value = Math.Clamp(position.TotalSeconds, 0, maximum);
                UpdateTimeText(position, duration);
            }
            else if (now < _pendingSeekDeadline)
            {
                TimelineSlider.Value = Math.Clamp(targetSeconds, 0, maximum);
                UpdateTimeText(TimeSpan.FromSeconds(targetSeconds), duration);
            }
            else
            {
                _pendingSeekTargetSeconds = null;
                _suppressPositionPollingUntil = DateTimeOffset.MinValue;
                PlaybackStatusText.Text = _playback.LastDiagnosticMessage is { Length: > 0 } message
                    ? $"Seek not confirmed: {message}"
                    : "Seek not confirmed";
                TimelineSlider.Value = Math.Clamp(position.TotalSeconds, 0, maximum);
                UpdateTimeText(position, duration);
            }
        }
        else if (DateTimeOffset.UtcNow >= _suppressPositionPollingUntil)
        {
            TimelineSlider.Value = Math.Clamp(position.TotalSeconds, 0, maximum);
            UpdateTimeText(position, duration);
        }

        _suppressVolumeChange = true;
        VolumeSlider.Value = Math.Clamp(_playback.Volume, 0, 100);
        _suppressVolumeChange = false;

        PlayPauseButton.Content = _playback.IsPaused ? "Play" : "Pause";
        UpdateSeekDebugText();
    }

    private void UpdateSeekDebugText()
    {
        if (_playback is null)
        {
            SeekDebugText.Text = "Seek diagnostics: playback session not ready";
            return;
        }

        SeekDebugText.Text =
            $"UI Duration: {_playback.Duration.TotalSeconds:0.###}s | " +
            $"UI Position: {_playback.Position.TotalSeconds:0.###}s | " +
            $"Seekable: {_playback.Seekable} | " +
            $"Paused: {_playback.IsPaused} | " +
            $"Pending target: {(_pendingSeekTargetSeconds is null ? "<none>" : $"{_pendingSeekTargetSeconds.Value:0.###}s")}\n" +
            (_playback.DebugSnapshot ?? "mpv debug snapshot: <none>");
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

    private void ShowContextMenuPlaceholder()
    {
        ContextMenuNotice.IsVisible = true;
        _contextNoticeTimer.Stop();
        _contextNoticeTimer.Start();
    }

    private static void Report(string message)
    {
        var fullMessage = $"[Auralith.App] {message}";
        Console.WriteLine(fullMessage);
        System.Diagnostics.Debug.WriteLine(fullMessage);
    }
}
