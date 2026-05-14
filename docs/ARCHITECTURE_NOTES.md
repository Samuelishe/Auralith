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

Do not create these projects yet. Avoid adding more layers before implementation pressure exists. When code work is explicitly approved, prefer the smallest useful starting set:

```text
src/
  Auralith.App/
  Auralith.Core/
  Auralith.Playback/
  Auralith.Playback.Mpv/
```

Add `Auralith.Media`, `Auralith.UI`, `Auralith.UI.DesignSystem`, `Auralith.Infrastructure`, and test projects only when real implementation pressure exists. Do not create empty semantic projects for architectural appearance.

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

Phase 1 implementation pressure:

- Embedded Avalonia rendering needs a visual control hosted by the app shell.
- The current spike allows `Auralith.App` to host `Auralith.Playback.Mpv.MpvPlaybackSurface`.
- `Auralith.App` must not reference `MpvView`, `MpvContext`, or Hanuman APIs directly.
- This keeps concrete binding code inside `Auralith.Playback.Mpv` while avoiding speculative factories or DI layers.
- Native libmpv probing and `MpvApi.RootPath` configuration belong inside `Auralith.Playback.Mpv`, not in the app shell.
- Dev-time native probing is not release packaging. Future bundling should be handled by packaging work, not by spreading native-runtime decisions through UI code.
- Dev-only native runtime acquisition belongs in `tools/`, not in application startup.

## File Input Boundary

The current spike supports only a narrow media-open path:

```text
App/MainWindow receives a candidate file path
-> Auralith.Core validates it as a MediaOpenRequest
-> MainWindow passes the validated path to the active PlaybackSession
-> Playback layer opens media
```

This boundary is intentionally small. It does not introduce a media library, import system, playlist parser, recent files model, folder scanner, or metadata pipeline.

Supported/current input directions:

- File picker / Open command.
- Command-line file path, used later by OS `Open with` integration.
- Single-file drag/drop.

Future packaging concern:

- Windows file association registration.
- Linux `.desktop` MIME registration.

`Open with Auralith` should remain split into app capability and OS integration. The app can accept a file path; installers/packages may later register file associations.

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

## Preliminary Ownership Model

This is planning guidance, not implemented architecture.

`MainWindow` owns:

- Window state.
- Fullscreen state.
- Window size and position.
- Active presentation mode.

`MediaShell` owns:

- Composition of presentation modes.
- Switching between Video Presentation Mode and Audio Presentation Mode.
- Shared transport area placement.

`PlaybackSession` owns:

- Current media.
- Playback state.
- Position.
- Duration.
- Volume.
- Track lists.
- Subtitle state.
- Media state/events.

`PlaybackQueue` owns:

- Current queue item.
- Next/previous.
- History.
- Future shuffle/repeat behavior.

`VideoPresentation` owns:

- Overlay visibility.
- Video surface interaction behavior.
- Idle timer.
- Local video UX state.

Fullscreen is window/shell state, not playback engine state. The playback engine should not know why the user toggled fullscreen. It should expose playback facts and commands, not UI intent.

## Future Questions

- What is the smallest useful main shell that can host both presentation modes?
- What is the smallest useful playback abstraction over libmpv?
- How much mpv behavior should remain directly configurable?
- What belongs in persistent SQLite state versus derived cache?
- How should metadata providers be sandboxed or trusted?
- Where should shared queue/session state live without creating speculative infrastructure?
