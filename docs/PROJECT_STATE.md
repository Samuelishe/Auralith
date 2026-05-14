# Project State

Date: 2026-05-14

## Current Phase

Phase 1 controlled technical validation.

Phase 0 documentation foundation is complete enough to support controlled Phase 1 work.

Phase 1 has started in a deliberately narrow form: minimal skeleton, minimal Avalonia application foundation, and first playback integration spike only.

## Repository Status

- Solution exists: `Auralith.sln`.
- Documentation folder exists: `docs`.
- Minimal `src/` skeleton exists: `Auralith.App`, `Auralith.Core`, `Auralith.Playback`, `Auralith.Playback.Mpv`.
- No `tests/` projects have been created yet.
- Minimal Avalonia application foundation exists.
- Minimal Hanuman/libmpv playback spike code exists.
- Minimal xUnit v3 + Shouldly test foundation exists for Core and Playback.
- Build succeeds on .NET 10.
- Tests pass on .NET 10.
- Runtime startup succeeds in controlled failure mode when native `libmpv` is missing.
- Actual embedded playback is not yet validated because native `libmpv.2` is not available in the app output/runtime path.

## Current Scope

In scope:

- Documentation structure.
- Agent onboarding rules.
- Project vision.
- Initial roadmap.
- Initial decision log.
- Separation of confirmed direction from speculative ideas.
- Unified media shell concept planning.
- Minimal Phase 1 technical validation.
- Native libmpv loading investigation.
- Lightweight unit testing for non-native Core/Playback logic.

Out of scope:

- Broad Avalonia UI implementation.
- Separate audio/video player projects.
- Full playback engine.
- Full media player implementation.
- Placeholder interfaces/classes/services/viewmodels.
- MVVM structure.
- DI setup.
- SQLite layer.
- Service layer.
- Control library.
- UI/native playback automated testing.

## Continuity Goals

Future sessions should be able to answer:

- What is Auralith trying to become?
- What phase is active?
- What has already been decided?
- What remains speculative?
- What should not be implemented yet?
- Where should new findings be recorded?
