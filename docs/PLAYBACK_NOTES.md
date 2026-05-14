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
- The selected binding's native loading expectation has been identified:
  - Windows: `libmpv-2.dll`.
  - Linux: `libmpv.so.2`.
- `Auralith.Playback.Mpv` now probes a small set of development-time native locations and sets `MpvApi.RootPath` when a matching native library is found.
- File opening requests can be initiated through the file picker, command-line media path, or a single-file drag/drop.
- `tools/setup-libmpv-windows.ps1` can populate `runtimes/win-x64/native` from a trusted Windows mpv/libmpv build source.
- A local run with a video from `E:\Downloads\Films` stayed alive after adding a Windows application manifest.

Failed / blocked:

- Before adding the manifest, Avalonia `NativeControlHost` failed on Windows with: "Unable to create child window for native control host. Application manifest with supported OS list might be required."
- Embedded video rendering, overlay-on-video behavior, duration/position sync, seek, volume, fullscreen-with-video, and live video interactions still require visual/manual validation.

Assumption change:

- Native dependency loading is not a later packaging concern only. It is an immediate blocker for playback validation on Windows.

## Native libmpv Loading Strategy

This is a Phase 1 development-time strategy, not a full packaging system.

The current Hanuman binding resolves libmpv through `MpvApi.RootPath`. `Auralith.Playback.Mpv` configures that root path before creating the embedded `MpvView`.

Windows development strategy:

- Provide `libmpv-2.dll` and its required native companion DLLs locally.
- Preferred setup command:

```powershell
.\tools\setup-libmpv-windows.ps1
```

- Use a compatible Windows libmpv/mpv build that provides `libmpv-2.dll`; no single Windows DLL source has been locked as the project standard yet.
- Preferred repository-local path for the spike: `runtimes/win-x64/native`.
- Also supported: place the native files next to the app output.
- Do not commit native DLLs unless a future packaging/licensing decision explicitly approves that.
- If the native library is missing, the app should show a controlled failure message instead of crashing.
- Future Windows releases should bundle `libmpv-2.dll` and companion DLLs so normal users can launch Auralith without manually installing mpv/libmpv.

Trusted source policy:

- Use Windows builds listed by the official mpv installation page.
- Current dev helper defaults to shinchiro GitHub releases.
- zhongfly GitHub releases are supported by `-Source zhongfly`.
- Do not use random DLL download sites, "missing DLL" sites, Softonic-like mirrors, or opaque file hosts.

Local validation result:

- Source used: shinchiro `mpv-dev-x86_64-20260421-git-5921fe5.7z`.
- `libmpv-2.dll` was copied to `runtimes/win-x64/native`.
- A command-line launch with `E:\Downloads\Films\Неуместный человек (Den brysomme mannen; The Bothersome Man) [2006] 1080p BDRemux-ARTiCUN0.mkv` stayed alive for 15 seconds and was stopped manually.
- Visual playback, overlay, seek, volume, fullscreen, resize, and drag/drop still need human confirmation.

Current Windows missing-runtime message should explain:

- Native libmpv runtime is missing.
- Expected file name is `libmpv-2.dll`.
- Auralith looked in `runtimes/win-x64/native` and next to the app output.
- This is a Phase 1/dev-time blocker.
- Future Windows releases should bundle the runtime.

Linux / Arch development strategy:

- Initial direction is system libmpv through the package manager.
- The current expected native library name is `libmpv.so.2`.
- Full AppImage, Flatpak, or bundled-native packaging remains future work.

Manual playback validation needs:

- A local video file outside the repository.
- A compatible native libmpv runtime available through the strategy above.
- No large media assets should be added to git.

## File Input Model

Current app capability:

- File picker / `Open` command can request one local file.
- Command-line argument can request one local file:

```powershell
dotnet run --project src/Auralith.App/Auralith.App.csproj -- C:\path\to\file.mp4
```

- Single-file drag/drop onto the video surface can request one local file.

Current constraints:

- Folder opening is rejected in this spike.
- Missing or invalid paths produce controlled validation errors.
- Multiple dropped files are ignored with a controlled message.
- No playlist, queue, media library, recent files, metadata import, or folder scanning behavior is implied.

Future `Open with` support:

- App capability depends on command-line path handling, which now exists.
- OS integration is separate: Windows file associations and Linux `.desktop` MIME associations belong to future packaging/installer work.

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
- Release packages should include `libmpv-2.dll` and required companion DLLs once licensing and packaging decisions are made.
- Phase 1 does not include an installer, updater, automatic downloader, or release automation.

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
