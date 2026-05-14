# Architecture Notes

This file captures architectural boundaries and cautions before implementation starts. It should not become a fake architecture document for code that does not exist.

## Current Stance

No production architecture has been implemented. Do not invent project folders, services, view models, or placeholder abstractions during the documentation phase.

## Known Direction

The intended application stack implies these future concerns:

- Avalonia UI shell.
- MVVM presentation structure.
- Dependency injection through `Microsoft.Extensions.DependencyInjection`.
- libmpv-backed playback.
- SQLite-backed local state where appropriate.
- TagLibSharp for local media metadata.
- Serilog for structured logging.
- Future metadata provider/plugin boundaries.

## Architecture Principle

Architecture should emerge from real seams in the application, not from speculative layering. Before implementation starts, document decisions and constraints. During implementation, prefer narrow vertical slices over large abstract frameworks.

## Future Questions

- How should audio and video windows share playback state, if at all?
- What is the smallest useful playback abstraction over libmpv?
- How much mpv behavior should remain directly configurable?
- What belongs in persistent SQLite state versus derived cache?
- How should metadata providers be sandboxed or trusted?
