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
- Minimal test projects exist: `Auralith.Core.Tests` and `Auralith.Playback.Tests`.
- Minimal Avalonia application foundation exists.
- Minimal Hanuman/libmpv playback spike code exists.
- Minimal xUnit v3 + Shouldly test foundation exists for Core and Playback.
- Build succeeds on .NET 10.
- Tests pass on .NET 10.
- Runtime startup is designed to remain in controlled failure mode when native `libmpv` is missing.
- Windows dev-time native probing now looks for `libmpv-2.dll` next to the app output or under `runtimes/win-x64/native`.
- Actual embedded playback is not yet validated because native `libmpv-2.dll` and its companion DLLs are not available in the local runtime path.
- File input app capability now exists for file picker, command-line media path, and single local file drag/drop.
- OS-level `Open with Auralith` registration is not implemented and remains a future packaging concern.

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
- Minimal file input coordination for the current playback spike.

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
- OS file association registration.
- Media library, playlist import, recent files, folder scanning, and metadata extraction.

## Continuity Goals

Future sessions should be able to answer:

- What is Auralith trying to become?
- What phase is active?
- What has already been decided?
- What remains speculative?
- What should not be implemented yet?
- Where should new findings be recorded?
