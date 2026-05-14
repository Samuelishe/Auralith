# Session Log

## 2026-05-14 - Documentation Foundation

Context:

- Initial repository existed with a solution file and no application code.
- The user requested documentation and organizational groundwork only.

Changed:

- Added `AGENTS.md` as the main LLM/Codex onboarding and workflow file.
- Added `docs` with index, vision, state, roadmap, decisions, TODO, and topic notes.
- Added documentation files as solution items in `Auralith.sln` for Rider visibility.

Notes:

- Phase 0 is active.
- Production code remains out of scope.
- Confirmed decisions are separated from speculative ideas.

Next:

- Project owner should review the documentation foundation.
- Do not begin Phase 1 until explicitly approved.

## 2026-05-14 - Ignore Rules And Commit Summary Rule

Context:

- The repository needed ignore rules appropriate for future .NET/Avalonia development.
- The project owner requested a standing rule that each agent session ends with a short English commit description.

Changed:

- Expanded `.gitignore` for build output, IDE state, NuGet artifacts, test output, logs, and OS files.
- Added the commit description requirement to `AGENTS.md`.

Notes:

- `.idea/` is intentionally ignored and should not be committed.

Next:

- Keep future commits limited to source, documentation, and intentional project files.

## 2026-05-14 - Unified Media Shell Model

Context:

- The project owner clarified that Auralith should not be built as separate audio and video player windows or projects.
- The project is still in documentation/concept planning only.

Changed:

- Updated product, UI, architecture, playback, theming, roadmap, TODO, and decision docs around a unified media shell.
- Accepted adaptive Audio and Video Presentation Modes as the primary model.
- Recorded queue-first playback, non-blocking metadata enrichment, and thin libmpv abstraction guidance.

Notes:

- `MainWindow`/main media shell is the planned primary interaction point.
- Audio and video presentation share playback/session/control foundations.
- Planned Phase 1 solution structure remains a planning direction only and has not been created.

Next:

- Review and finalize Phase 1 solution structure before creating any projects.
- Define the first vertical slice and playback spike questions.

## 2026-05-14 - Video Interaction, Processing, And Tray Planning

Context:

- The project owner added requirements for video surface interaction, future media processing, tray behavior, and platform priority.
- The project remains in documentation/concept planning only.

Changed:

- Documented single click/tap play-pause and double-click fullscreen for video.
- Added future hold-to-seek exploration and conflict rules for video interactions.
- Added planning notes for audio equalizer, video adjustments, audio normalization, dynamic range compression, and dialogue clarity.
- Added tray behavior expectations and platform priority guidance.

Notes:

- Processing features must wait for libmpv playback spike validation.
- Tray behavior must be documented honestly where Linux desktop environment support varies.

Next:

- Investigate libmpv audio/video processing capabilities.
- Investigate Avalonia/system tray cross-platform options.
- Define video interaction conflict rules before UI implementation.

## 2026-05-14 - Playback Integration And Phase 1 Planning

Context:

- The project owner refined playback integration strategy and Phase 1 planning boundaries.
- The project remains documentation/planning only.

Changed:

- Documented `HanumanInstitute.LibMpv` and `HanumanInstitute.LibMpv.Avalonia` as primary playback spike candidates.
- Added fallback/avoid lists for playback integration options.
- Added dependency isolation rules so UI layers do not depend directly on concrete libmpv APIs.
- Added playback spike acceptance criteria, native packaging direction, and Phase 1 planning constraints.
- Recorded native packaging risk and vertical-slice-first architecture guidance.

Notes:

- No dependencies, projects, placeholders, or production code should be created in Phase 0.
- Playback architecture must remain thin and evidence-driven until the spike validates assumptions.

Next:

- Finalize playback spike acceptance criteria.
- Define native libmpv loading and packaging strategy for Windows and Linux.
- Define shell, overlay, transport, timeline, fullscreen, tray, and interaction ownership boundaries.
