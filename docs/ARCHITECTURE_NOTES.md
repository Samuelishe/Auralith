# Architecture Notes

This file captures architectural boundaries and cautions before implementation starts. It should not become a fake architecture document for code that does not exist.

## Current Stance

No production architecture has been implemented. Do not invent project folders, services, view models, or placeholder abstractions during the documentation phase.

Auralith intentionally avoids enterprise architecture theatre, speculative abstraction layers, fake implementation progress, premature MVVM scaffolding, and fragmented audio/video player architecture.

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

Do not create these projects yet. Avoid adding more layers before implementation pressure exists.

## Future Dependency Direction

Planned dependency direction:

```text
Auralith.App
  -> Auralith.Playback
     -> thin practical playback abstractions
        -> Auralith.Playback.Mpv
           -> concrete libmpv binding
```

Rules:

- UI layers must never directly depend on concrete libmpv APIs.
- `HanumanInstitute.LibMpv` and `HanumanInstitute.LibMpv.Avalonia` must stay isolated behind playback boundaries if selected during the spike.
- Replacing the mpv binding should primarily affect `Auralith.Playback.Mpv`.
- Playback abstraction should emerge from validated use cases and vertical slices, not speculative backend-agnostic ambitions.
- Avoid pretending the project is backend-agnostic before implementation evidence exists.

## Architecture Principle

Architecture should emerge from real seams in the application, not from speculative layering. Before implementation starts, document decisions and constraints. During implementation, prefer narrow vertical slices over large abstract frameworks.

MVVM complexity and speculative infrastructure growth are known future risks. Favor vertical slices and practical boundaries over architectural purity.

Do not create `IUniversalMediaEngine`, backend-agnostic abstraction trees, plugin-based playback backends, or speculative transport orchestration layers unless future implementation evidence proves real need.

## Future Ownership Boundaries To Define

Before implementation, prepare documentation around:

- Shell composition.
- Presentation mode switching.
- Overlay layering.
- Transport bar ownership.
- Timeline ownership.
- Fullscreen state ownership.
- Tray ownership.
- Window lifecycle.
- Interaction conflict matrix.

## Future Questions

- What is the smallest useful main shell that can host both presentation modes?
- What is the smallest useful playback abstraction over libmpv?
- How much mpv behavior should remain directly configurable?
- What belongs in persistent SQLite state versus derived cache?
- How should metadata providers be sandboxed or trusted?
- Where should shared queue/session state live without creating speculative infrastructure?
