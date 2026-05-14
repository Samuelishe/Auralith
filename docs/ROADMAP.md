# Roadmap

This roadmap is phase-oriented. It is not a promise that every future idea will be implemented.

## Phase 0 - Documentation And Continuity

Status: Complete Enough For Phase 1

Phase 0 documentation foundation is complete enough for controlled Phase 1 work.

Goals:

- Create project documentation foundation.
- Define agent workflow.
- Capture project vision and design direction.
- Separate confirmed decisions from ideas.
- Keep repository free of premature production code.

Exit criteria:

- `AGENTS.md` exists as the session entry point.
- `docs` contains project memory and planning documents.
- Rider solution shows documentation items.
- Initial decisions, state, roadmap, and session log exist.

## Phase 1 - Structure And Spike Preparation

Status: Active

Phase 1 is active only as a constrained technical validation phase. It is not broad implementation approval.

Possible goals:

- Review and finalize .NET 10 solution/project structure.
- Choose Avalonia template strategy.
- Define build/test baseline.
- Define dependency policy.
- Define first vertical slice.
- Define playback spike acceptance criteria.
- Define native libmpv loading and packaging expectations.
- Create project structure only after explicit approval.
- Add Avalonia application project only after explicit approval.
- Establish formatting and build verification.
- Add dependency versions intentionally.

Current validation status:

- Minimal project skeleton created.
- Minimal Avalonia application foundation created.
- Minimal Core/Playback test projects created.
- Build succeeds on .NET 10.
- Tests pass on .NET 10.
- `.gitattributes` defines repository line-ending policy.
- Hanuman/libmpv binding is isolated in `Auralith.Playback.Mpv`.
- App startup no longer crashes when native `libmpv` is missing.
- Windows dev-time native probing is defined for `libmpv-2.dll`.
- Windows dev-time setup helper exists for trusted mpv/libmpv builds.
- Windows release direction remains bundled native runtime so normal users should not manually install mpv/libmpv.
- File picker, command-line file argument, and single-file drag/drop are supported as current app-level media-open inputs.
- Local `libmpv-2.dll` was prepared through the dev helper.
- The app stayed alive during a short command-line launch with a local video file.
- Embedded rendering and controls still require visual/manual validation.

Planned structure to review before creation:

```text
src/
  Auralith.App/
  Auralith.Core/
  Auralith.Playback/
  Auralith.Playback.Mpv/
  Auralith.Media/
  Auralith.UI/
  Auralith.UI.DesignSystem/
  Auralith.Infrastructure/

tests/
  Auralith.Core.Tests/
  Auralith.Playback.Tests/
```

Avoid separate `Auralith.AudioPlayer` and `Auralith.VideoPlayer` projects.

Avoid adding more layers before implementation pressure exists.

When implementation is explicitly approved, the first code step should be minimal skeleton creation, not full architecture buildout. Preferred initial set:

- `src/Auralith.App`
- `src/Auralith.Core`
- `src/Auralith.Playback`
- `src/Auralith.Playback.Mpv`

Other planned projects should wait until real implementation pressure exists.

## Phase 2 - UI Direction Prototype

Status: Future

Possible goals:

- Explore non-production UI prototypes or design references.
- Define design tokens.
- Validate unified media shell and adaptive presentation mode concepts.
- Define click, double-click, and future hold interaction model for the video surface.
- Define conflict rules for video interactions, including context menu, fullscreen, drag, subtitle selection, and overlay controls.

## Phase 3 - Playback Spike

Status: Active/Blocked By Native libmpv Availability

Possible goals:

- Investigate `HanumanInstitute.LibMpv` and `HanumanInstitute.LibMpv.Avalonia` as primary playback spike candidates.
- Consider `MPVSharp` or manual P/Invoke only if the primary candidates fail badly.
- Validate cross-platform packaging implications.
- Identify playback API boundaries.
- Validate thin playback abstraction and event/property mapping.
- Validate embedded Avalonia rendering, overlay z-order, resize/fullscreen behavior, timeline sync, and direct video surface interactions.
- Validate subtitle/audio track enumeration and switching.
- Investigate libmpv support for audio filters, equalizer, normalization, dynamic range compression, and replaygain/loudness options.
- Investigate libmpv support for video adjustments.

Current blocker:

- `HanumanInstitute.LibMpv.Avalonia` expects native `libmpv-2.dll` on Windows.
- Dev-time native setup is available, but final Windows release packaging remains unimplemented.
- Embedded video render, overlay z-order, seek, volume, fullscreen, and resize behavior still need manual validation with the prepared runtime.
- Native DLLs are not committed yet; distribution and licensing must be reviewed before bundled binaries are added.

Current file input status:

- File picker and command-line media path support exist for the current spike.
- Single-file drag/drop exists for the current spike.
- OS-level `Open with Auralith` registration is future packaging work and is not part of this phase.

Testing boundary:

- Unit tests cover non-native Core/Playback logic only.
- Native playback validation remains manual/spike work until libmpv loading and embedded rendering are stable enough to justify integration tests.

Avoid as primary direction:

- `Mpv.NET`.
- `LibVLCSharp`.
- Avalonia Pro `MediaPlayer`.
- Large custom playback engine before validation.

## Phase 4 - Application Foundation

Status: Future

Possible goals:

- Implement the first real application slice.
- Introduce MVVM, DI, logging, and persistence only where needed.

Recommended first vertical slice, approved only for the current constrained spike:

- Open video file.
- Show main media window.
- Enter Video Presentation Mode.
- Start libmpv playback.
- Show overlay controls.
- Support timeline, play/pause, seek, volume, and fullscreen.

This slice is video-first because it validates embedded rendering, overlay controls, fullscreen, native libmpv loading, timeline sync, resize behavior, and input interactions. Audio mode, playlists, metadata, tray, themes, equalizer, normalization, and video adjustments must not block it.

## Phase 5 - Feature Growth

Status: Future

Possible goals:

- Metadata experience.
- Playlists and queue.
- Audio Presentation Mode.
- Subtitles and audio track switching.
- Tray behavior for audio playback where platform support allows it.
- Audio equalizer and normalization after playback validation.
- Video adjustment controls after playback validation.
- Theme variants.
- Provider/plugin architecture.

## Platform Capability Planning

Status: Future

The project should eventually maintain a feature capability matrix for Windows 11, Windows 10, and modern Linux desktop environments. Tray behavior is the first known area where full parity may not be realistic.
