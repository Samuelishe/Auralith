# Architecture Notes

This file captures architectural boundaries and cautions before implementation starts. It should not become a fake architecture document for code that does not exist.

## Current Stance

No production architecture has been implemented. Do not invent project folders, services, view models, or placeholder abstractions during the documentation phase.

## Known Direction

The intended application stack implies these future concerns:

- Avalonia main media shell.
- Adaptive Audio and Video Presentation Modes inside one shell.
- Shared playback/session/control foundations.
- MVVM presentation structure.
- Dependency injection through `Microsoft.Extensions.DependencyInjection`.
- libmpv-backed playback.
- SQLite-backed local state where appropriate.
- TagLibSharp for local media metadata.
- Serilog for structured logging.
- Future metadata provider/plugin boundaries.

## Planned Phase 1 Solution Direction

This is a planning direction for Phase 1, not implemented architecture:

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

Do not create `Auralith.AudioPlayer` or `Auralith.VideoPlayer` projects. Audio and video are presentation modes of the same player shell, not separate applications.

## Architecture Principle

Architecture should emerge from real seams in the application, not from speculative layering. Before implementation starts, document decisions and constraints. During implementation, prefer narrow vertical slices over large abstract frameworks.

## Future Questions

- What is the smallest useful main shell that can host both presentation modes?
- What is the smallest useful playback abstraction over libmpv?
- How much mpv behavior should remain directly configurable?
- What belongs in persistent SQLite state versus derived cache?
- How should metadata providers be sandboxed or trusted?
- Where should shared queue/session state live without creating speculative infrastructure?
