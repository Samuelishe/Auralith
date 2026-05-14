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

## Phase 3 - Playback Spike

Status: Future

Possible goals:

- Investigate libmpv integration options.
- Validate cross-platform packaging implications.
- Identify playback API boundaries.
- Validate thin playback abstraction and event/property mapping.

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
- Theme variants.
- Provider/plugin architecture.
