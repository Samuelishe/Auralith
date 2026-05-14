# Playback Notes

Playback direction is known, but playback architecture is not implemented yet.

## Confirmed Direction

- libmpv is the playback backend.
- FFmpeg support comes through libmpv.
- Auralith should not reimplement decoding or media playback primitives.
- Subtitles and audio track switching are important product goals.
- Playback Queue is the primary runtime model.
- Playlists may feed the queue, but queue owns next/previous, shuffle, and history behavior.
- Metadata loading must not block playback: open file, start playback quickly, then enrich metadata asynchronously.
- Auralith should not drift toward VLC-centric architecture unless major playback validation fails.

## Playback Spike Candidates

Primary candidates:

- `HanumanInstitute.LibMpv`
- `HanumanInstitute.LibMpv.Avalonia`

Reasoning:

- Better fit for Avalonia + libmpv direction.
- Suitable for embedded playback experiments.
- Reduces need for immediate manual native interop work.
- Likely faster path toward validating playback architecture assumptions.

This is a playback spike candidate, not a permanent irreversible dependency commitment.

Fallback:

- `MPVSharp`.

Last resort:

- Manual P/Invoke over libmpv only if existing bindings fail badly.

Avoid as primary direction:

- `Mpv.NET`.
- `LibVLCSharp`.
- Avalonia Pro `MediaPlayer`.
- Large custom playback engine before validation.

## Current Spike Result

Date: 2026-05-14

Validated:

- .NET 10 solution with the minimal approved skeleton builds successfully.
- `HanumanInstitute.LibMpv` / `HanumanInstitute.LibMpv.Avalonia` package integration compiles.
- Concrete Hanuman/libmpv usage is isolated in `Auralith.Playback.Mpv`.
- The Avalonia app can start in a controlled failure mode when native libmpv is missing.

Failed / blocked:

- Runtime native loading fails without a compatible `libmpv.2` available to the app.
- Embedded playback, overlay-on-video behavior, duration/position sync, seek, volume, fullscreen-with-video, and live video interactions remain unvalidated until native libmpv is supplied.

Assumption change:

- Native dependency loading is not a later packaging concern only. It is an immediate blocker for playback validation on Windows.

## Thin Playback Abstraction

The abstraction over libmpv should stay practical and narrow. Avoid a large speculative `IUniversalMediaEngine`.

Expected future surface:

- Play, pause, stop.
- Seek.
- Volume.
- Duration and position.
- Media state.
- Track lists.
- Subtitle selection.
- Audio track selection.
- Events/properties mapped from libmpv where useful.

UI layers must not depend directly on concrete libmpv binding APIs. Binding-specific code belongs behind future playback boundaries, most likely in `Auralith.Playback.Mpv`.

## Playback Spike Acceptance Criteria

Playback:

- Open media file.
- Video playback.
- Audio playback.
- Play, pause, and stop.
- Seek.
- Smooth timeline updates.
- Duration/position sync.
- Volume control.
- Fullscreen.
- Resize behavior.

Video surface:

- Embedded rendering inside Avalonia.
- Overlay controls above video.
- Z-order behavior.
- Click play/pause.
- Double-click fullscreen.
- Future interaction feasibility.

Tracks/subtitles:

- Subtitle enumeration.
- Subtitle switching.
- External subtitle loading feasibility.
- Audio track enumeration.
- Audio track switching.

Performance:

- Acceptable CPU usage.
- Acceptable GPU/render behavior.
- No obvious rendering instability.

Platform:

- Native library loading on Windows.
- Native library loading on Linux.
- Packaging implications.
- Bundled versus system libmpv behavior.

Processing feasibility:

- Equalizer support.
- Replaygain/loudness normalization.
- Dynamic range compression/dialogue clarity.
- Brightness.
- Contrast.
- Saturation.
- Gamma.
- Possible sharpness.

## Future Concerns

- libmpv lifecycle ownership.
- Cross-platform native library loading.
- Packaging for Windows and Linux.
- Mapping mpv events into application state.
- Error reporting and recoverability.
- Subtitle discovery, selection, and styling boundaries.
- Audio track enumeration and switching.
- Timeline precision and buffering state.
- Queue/session ownership.
- Async metadata enrichment.
- Audio equalizer support through mpv/FFmpeg filters.
- Audio normalization, replaygain, loudness normalization, dynamic range compression, and dialogue clarity options.
- Video adjustment support for brightness, contrast, saturation, gamma, and possibly sharpness.
- Clear UX boundaries for processing controls so they do not become settings clutter.
- Native packaging and runtime dependency handling.

## Preliminary Packaging Direction

Native packaging and runtime dependency handling are expected to be one of the highest-risk technical areas of the project.

Windows:

- Bundled native libmpv is preferred.
- Users should not manually install mpv.
- Windows 11 receives the highest polish priority.

Linux / Arch:

- Initial direction may rely on system libmpv.
- On Arch Linux, system `libmpv` installed through the package manager is an acceptable initial direction.
- Future AppImage/Flatpak investigation is possible.
- Linux desktop integration limitations should be documented honestly.

## Media Processing Planning

Audio and video processing features are planned but spike-dependent.

Audio processing should investigate:

- Equalizer support.
- ReplayGain or loudness normalization for music.
- Dynamic range compression or dialogue clarity for video.
- mpv/FFmpeg filter availability and cross-platform behavior.

Video processing should investigate:

- Brightness.
- Contrast.
- Saturation.
- Gamma.
- Sharpness if backend support is practical.

Do not implement processing controls before the libmpv playback spike confirms capabilities, performance implications, and UX boundaries.

## First Slice Boundaries

Recommended first implementation slice, after explicit approval:

```text
Open video file
-> MainWindow / unified media shell
-> Video Presentation Mode
-> libmpv playback
-> overlay controls
-> play/pause
-> seek
-> volume
-> fullscreen
```

Video-first is recommended because it validates the riskiest areas:

- Embedded rendering.
- Overlay controls.
- Fullscreen.
- Native libmpv loading.
- Timeline sync.
- Resize behavior.
- Input interactions.

Audio mode, playlists, metadata, tray behavior, themes, equalizer, normalization, and video adjustments remain future/planned work. They should inform boundaries but must not block the first slice.

## Non-Goals For Current Phase

- No playback wrapper.
- No mpv binding choice.
- No playback service.
- No NuGet package installation.
- No universal media engine abstraction.
- No backend-agnostic abstraction tree.
- No plugin-based playback backend system.
- No speculative transport orchestration layer.
- No test media fixtures.
- No native dependency packaging.
- No equalizer, normalization, dynamic range, or video adjustment implementation.
