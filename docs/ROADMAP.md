# Roadmap

This roadmap is phase-oriented. It is not a promise that every future idea will be implemented.

## Phase 0 - Documentation And Continuity

Status: Active

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

## Phase 1 - Project Skeleton

Status: Future

Possible goals:

- Review and finalize .NET 10 solution/project structure.
- Choose Avalonia template strategy.
- Define build/test baseline.
- Define dependency policy.
- Define first vertical slice.
- Create project structure only after explicit approval.
- Add Avalonia application project only after explicit approval.
- Establish formatting and build verification.
- Add dependency versions intentionally.

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

## Phase 2 - UI Direction Prototype

Status: Future

Possible goals:

- Explore non-production UI prototypes or design references.
- Define design tokens.
- Validate unified media shell and adaptive presentation mode concepts.
- Define click, double-click, and future hold interaction model for the video surface.
- Define conflict rules for video interactions, including context menu, fullscreen, drag, subtitle selection, and overlay controls.

## Phase 3 - Playback Spike

Status: Future

Possible goals:

- Investigate libmpv integration options.
- Validate cross-platform packaging implications.
- Identify playback API boundaries.
- Validate thin playback abstraction and event/property mapping.
- Investigate libmpv support for audio filters, equalizer, normalization, dynamic range compression, and replaygain/loudness options.
- Investigate libmpv support for video adjustments.

## Phase 4 - Application Foundation

Status: Future

Possible goals:

- Implement the first real application slice.
- Introduce MVVM, DI, logging, and persistence only where needed.

Candidate first vertical slice, not approved for implementation yet:

- Open video file.
- Show main media window.
- Enter Video Presentation Mode.
- Start libmpv playback.
- Show overlay controls.
- Support timeline, play/pause, seek, volume, and fullscreen.

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
