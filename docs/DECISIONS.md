# Decisions

This log records durable decisions. Do not remove old decisions when direction changes; mark them Superseded and add a new entry.

## 2026-05-14 - Documentation-First Foundation

Status: Accepted

Context:

The project is at its start and needs continuity for long-running human and LLM-assisted development.

Decision:

Create a documentation-first foundation before any application implementation.

Consequences:

- No production code during Phase 0.
- Future sessions must read the documentation entry points before changing the project.
- Project knowledge is stored in Markdown and kept visible in Rider through solution items.

## 2026-05-14 - Modern Platform Target

Status: Accepted

Context:

Auralith aims to be modern rather than constrained by old platform compatibility.

Decision:

Target Windows 10/11+ and Linux, with Arch Linux treated as first-class.

Consequences:

- Old operating systems are not a design constraint.
- UI and packaging decisions may assume modern desktop capabilities.

## 2026-05-14 - Separate Audio And Video Windows

Status: Superseded

Context:

Audio and video playback have different interaction and information-density needs.

Decision:

Audio and video should use separate windows while sharing a single visual language.

Consequences:

- Audio can support richer metadata and playlist surfaces.
- Video can keep content central with minimal disappearing controls.
- Superseded by the unified media shell decision below.

## 2026-05-14 - Unified Media Shell With Adaptive Presentation Modes

Status: Accepted

Context:

Separate primary audio/video windows or projects would duplicate control logic, split playback state, and encourage two-player architecture.

Decision:

Auralith uses a unified main media window/main media shell with adaptive Audio and Video Presentation Modes. Opening video switches the shell into Video Presentation Mode. Opening audio switches it into Audio Presentation Mode. These are modes of one player shell, not separate players.

Consequences:

- Playback state, queue/session state, theme state, and base control logic remain shared.
- Audio and video presentation can adapt to content type while preserving a common visual language.
- Shared controls should be reused instead of duplicated.
- Do not create separate `AudioPlayerWindow`/`VideoPlayerWindow` as the primary architecture.
- Do not create separate `Auralith.AudioPlayer` and `Auralith.VideoPlayer` projects.

## 2026-05-14 - Themes Modify Feel, Not Structure

Status: Accepted

Context:

The project may support multiple style themes, but theme flexibility can easily damage maintainability.

Decision:

Themes may alter visual feel, density, motion, contrast, and material treatment, but must not change window structure or information architecture.

Consequences:

- Theme design must be token-driven and layout-stable.
- No skin system that replaces UI structure.
- Free-form skinning that can break layout is rejected.

## 2026-05-14 - Token-Based Theme System

Status: Accepted

Context:

Style themes are desirable, but the project must avoid completely different skins that fracture UX and maintenance.

Decision:

Themes modify visual feel through tokens, not window/control structure. Color themes and style themes are separate concepts. Style themes such as Minimal, Soft, Sharp, Compact, and Floating are controlled variants, not free-form skins.

Consequences:

- Themes may change colors, spacing, radius, stroke thickness, shadows, opacity, animation timings, density, overlay material feel, and timeline hover scale within safe layout bounds.
- Themes must not change layout, control tree, UX structure, or basic interaction model.
- Layout stability is more important than visual experimentation.

## 2026-05-14 - libmpv Playback Backend

Status: Accepted

Context:

Auralith needs mature audio/video playback with FFmpeg support.

Decision:

Use libmpv as the playback backend, with FFmpeg available through libmpv.

Consequences:

- Future playback work must account for libmpv lifecycle and platform packaging.
- The application should avoid reimplementing codec/playback responsibilities.

## 2026-05-14 - Queue As Runtime Playback Model

Status: Accepted

Context:

Playlists and opened files need a common runtime model for next/previous, shuffle, history, and session behavior.

Decision:

Playback Queue is the primary runtime model. Playlists may be sources for queue entries, but the queue controls runtime navigation and playback order.

Consequences:

- Queue/session design should be considered early.
- Playlists should not directly own all runtime playback behavior.
- Shuffle and history belong to queue/session behavior.

## 2026-05-14 - Non-Blocking Metadata Enrichment

Status: Accepted

Context:

Local and internet metadata can be slow, incomplete, or unavailable. Playback should feel immediate.

Decision:

Metadata loading and enrichment must not block playback. The expected flow is: open file, start playback quickly, then enrich metadata asynchronously.

Consequences:

- Playback startup path must stay separate from metadata enrichment.
- Metadata providers are optional future capability, not MVP playback dependency.

## 2026-05-14 - Avoid Large Speculative Playback Abstraction

Status: Accepted

Context:

The project will use libmpv, but wrapping it in a large generic engine before a spike would add architecture without evidence.

Decision:

Avoid building a large speculative `IUniversalMediaEngine`. Future playback abstraction should be thin and practical: play, pause, stop, seek, volume, duration, position, media state, track lists, subtitle selection, audio track selection, and useful events/properties.

Consequences:

- Playback API shape should be validated by libmpv spike and first vertical slice.
- Do not design an all-purpose media engine before implementation evidence exists.

## 2026-05-14 - Avoid Premature Production Implementation

Status: Accepted

Context:

The project is still in documentation/concept planning.

Decision:

Do not start production implementation before documentation decisions and planning are updated and the next phase is explicitly approved.

Consequences:

- No `src/` projects, Avalonia app, playback engine, or service architecture during Phase 0.
- Future implementation should begin from an agreed vertical slice, not broad speculative scaffolding.

## 2026-05-14 - Separate Primary Audio/Video Player Architecture

Status: Rejected

Context:

Auralith needs audio and video experiences, but splitting them into separate primary player architectures would duplicate shared behavior.

Decision:

Reject separate `AudioPlayerWindow`/`VideoPlayerWindow` as the primary architecture.

Consequences:

- Audio and video are adaptive presentation modes inside the unified media shell.
- Shared playback/session/control foundations are preferred.

## 2026-05-14 - Separate AudioPlayer/VideoPlayer Projects

Status: Rejected

Context:

Separate projects named around audio and video player applications would push the solution toward two applications instead of one player shell.

Decision:

Reject `Auralith.AudioPlayer` and `Auralith.VideoPlayer` projects.

Consequences:

- Phase 1 solution planning should use shared app, UI, playback, media, and infrastructure boundaries instead.
- Content-type behavior belongs in presentation modes and shared services only when implementation proves the need.

## 2026-05-14 - Free-Form Skinning

Status: Rejected

Context:

Completely free-form skins can break layout, interaction, accessibility, and long-term maintainability.

Decision:

Reject AIMP/Winamp-style free-form skins that can replace structure or control trees.

Consequences:

- Auralith theming must be token-driven and controlled.
- Style themes can change feel, not UX structure.

## 2026-05-14 - Large Speculative Playback Engine

Status: Rejected

Context:

A broad media-engine abstraction before the libmpv spike would add complexity without implementation evidence.

Decision:

Reject creating a large speculative universal playback engine before validating libmpv integration.

Consequences:

- Future playback abstraction should stay thin and shaped by vertical slices.
- The project should avoid pretending to be backend-agnostic before there is a concrete reason.

## 2026-05-14 - Production Code During Documentation Phase

Status: Rejected

Context:

The current phase is documentation/concept planning.

Decision:

Reject starting production implementation before the documentation decisions are updated and the next phase is explicitly approved.

Consequences:

- No `src/` projects or Avalonia app in Phase 0.
- No playback engine, windows, service layer, DI setup, or placeholder code during Phase 0.

## 2026-05-14 - Direct Video Surface Interaction

Status: Accepted

Context:

Video playback should feel modern and close to familiar web-player interaction patterns without adding permanent UI controls.

Decision:

Video surface interaction should support single click/tap to toggle play/pause and double click to toggle fullscreen.

Consequences:

- Video controls can remain minimal and overlay-based.
- Interaction design must avoid conflicts with subtitle selection, right-click context menus, drag/window move behavior, overlay controls, and text selection.
- Hold-to-seek left/right zones remain a future UX feature to explore, not an immediate MVP requirement.

## 2026-05-14 - Media Processing Features Require Playback Spike

Status: Accepted

Context:

Audio equalizer, video adjustments, normalization, and dynamic range behavior depend on libmpv/FFmpeg capabilities and UX boundaries.

Decision:

Advanced media processing features are planned, but must be validated through the libmpv playback spike before implementation.

Consequences:

- Investigate audio filters, equalizer, replaygain/loudness normalization, dynamic range compression, and dialogue clarity.
- Investigate video brightness, contrast, saturation, gamma, and possible sharpness controls.
- Do not implement processing UI or playback filters before capabilities and constraints are confirmed.

## 2026-05-14 - Tray Support Desired With Platform Differences

Status: Accepted

Context:

Audio playback benefits from background-friendly behavior, but system tray support varies across platforms and Linux desktop environments.

Decision:

Auralith should support minimizing to system tray where feasible, especially for audio playback. Tray behavior should include restore, play/pause, next/previous, and current track title if platform support allows.

Consequences:

- Windows 11 should receive the highest polish for tray behavior.
- Windows 10 is secondary.
- Linux/Arch support should be best-effort and documented honestly when desktop environment support varies.
- Do not overpromise Linux tray parity.

## 2026-05-14 - Platform Feature Polish Priority

Status: Accepted

Context:

Auralith targets Windows and Linux, but some desktop integration features may not work consistently across all platforms.

Decision:

When full cross-platform parity is impossible, feature polish priority is Windows 11, then Windows 10, then Arch Linux / modern Linux desktop environments.

Consequences:

- This priority may guide feature polish and fallback behavior.
- It must not justify careless Windows-only architecture.
- Platform-specific limitations should be documented honestly.

## 2026-05-14 - Planned Phase 1 Solution Structure

Status: Accepted

Context:

The project needs a direction for future project structure without creating empty projects prematurely.

Decision:

Use the documented Phase 1 structure as planning direction:

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

Consequences:

- This is not implemented architecture.
- Do not create all projects immediately.
- When code work is approved, prefer the minimal first set: `Auralith.App`, `Auralith.Core`, `Auralith.Playback`, and `Auralith.Playback.Mpv`.
- Add remaining projects only when implementation pressure exists.
- Do not create `Auralith.AudioPlayer`, `Auralith.VideoPlayer`, or empty semantic projects for appearance.

## 2026-05-14 - Recommended First Vertical Slice

Status: Accepted

Context:

The first implementation slice should validate the highest-risk assumptions before broader application growth.

Decision:

The recommended first slice is video-first: open video file -> main media shell -> Video Presentation Mode -> libmpv playback -> overlay controls -> play/pause -> seek -> volume -> fullscreen.

Consequences:

- This slice validates embedded rendering, overlay controls, fullscreen, native libmpv loading, timeline sync, resize behavior, and input interactions.
- Audio mode, playlists, metadata, tray, themes, equalizer, normalization, and video adjustments remain planned but must not block the first slice.
- This is a recommendation for future approved implementation, not current authorization to implement.

## 2026-05-14 - Preliminary Shell And Playback Ownership Model

Status: Accepted

Context:

Ownership boundaries need to be explicit before implementation to avoid playback/UI coupling.

Decision:

Use the preliminary ownership model documented in `ARCHITECTURE_NOTES.md`: `MainWindow` owns window/fullscreen/size/position/active mode; `MediaShell` owns presentation composition and switching; `PlaybackSession` owns playback facts and commands; `PlaybackQueue` owns runtime queue navigation; `VideoPresentation` owns local video UX state.

Consequences:

- Fullscreen is window/shell state, not playback engine state.
- Playback engine should expose playback facts and commands, not UI intent.
- Ownership may be refined by implementation evidence, but this is the starting planning model.

## 2026-05-14 - Shared Timeline Concept

Status: Accepted

Context:

Audio and video should not duplicate timeline behavior unnecessarily.

Decision:

Use one shared timeline concept/control, `AuralithTimeline`, with mode-specific visual styling such as `VideoTimelineStyle` and `AudioTimelineStyle`.

Consequences:

- MVP timeline scope is progress, seek, hover affordance, thin idle video style, and more visible hover/active state.
- Do not implement waveform, preview thumbnails, chapters, or advanced buffered visualization in the first slice.

## 2026-05-14 - Tray Is Planned But Not First Video Slice MVP

Status: Accepted

Context:

Tray support is useful for audio/background playback, but the first slice is video-first.

Decision:

Tray support is planned, especially for audio playback, but is not MVP for the first video slice.

Consequences:

- Windows 11 remains the primary polish target, Windows 10 secondary, and Linux/Arch best effort.
- Tray behavior belongs later with audio/background playback work.
- Linux desktop environment differences must be documented honestly.

## 2026-05-14 - Primary libmpv Spike Candidate

Status: Accepted

Context:

Auralith needs to validate embedded libmpv playback in Avalonia before architecture solidifies.

Decision:

Use `HanumanInstitute.LibMpv` and `HanumanInstitute.LibMpv.Avalonia` as the primary playback spike candidates.

Consequences:

- This is not a permanent irreversible dependency commitment.
- `MPVSharp` remains a fallback candidate.
- Manual P/Invoke over libmpv is a last resort if existing bindings fail badly.
- Avoid `Mpv.NET`, `LibVLCSharp`, Avalonia Pro `MediaPlayer`, and large custom playback engines as primary direction.
- The project should not drift toward VLC-centric architecture unless major playback validation fails.

## 2026-05-14 - Isolate Concrete libmpv Bindings

Status: Accepted

Context:

The UI should not become coupled to a specific mpv binding package.

Decision:

UI layers must not directly depend on concrete libmpv APIs. Concrete binding usage should stay behind playback boundaries, primarily in future `Auralith.Playback.Mpv`.

Consequences:

- Replacing the mpv binding should primarily affect `Auralith.Playback.Mpv`.
- Playback abstractions should remain thin and practical.
- Do not create backend-agnostic abstraction trees before evidence proves a real need.

## 2026-05-14 - Native Packaging Is A Major Risk Area

Status: Accepted

Context:

libmpv requires native runtime dependency handling, and platform behavior differs between Windows and Linux.

Decision:

Treat native packaging and runtime dependency loading as one of the highest-risk technical areas of the project.

Consequences:

- Windows should prefer bundled native libmpv so users do not manually install mpv.
- Linux/Arch may initially rely on system libmpv.
- Future AppImage/Flatpak investigation is possible.
- Packaging assumptions must be validated during playback spike planning.

## 2026-05-14 - Thin Evidence-Driven Playback Abstraction

Status: Accepted

Context:

Speculative playback abstractions can become architecture theatre before real libmpv constraints are known.

Decision:

Playback abstraction should stay thin and evidence-driven, emerging from validated use cases and vertical slices.

Consequences:

- Do not create `IUniversalMediaEngine`.
- Do not create plugin-based playback backends or speculative transport orchestration layers.
- Avoid pretending the project is backend-agnostic before implementation evidence exists.

## 2026-05-14 - Vertical Slices Over Architectural Purity

Status: Accepted

Context:

MVVM complexity and speculative infrastructure growth are known future risks.

Decision:

Favor vertical slices and practical boundaries over architectural purity.

Consequences:

- Phase 1 should remain structure planning, dependency planning, spike preparation, and build/test strategy planning.
- Do not create placeholder interfaces, classes, services, view models, DI setup, or MVVM scaffolding during planning.
- Add layers only when implementation pressure proves they remove real complexity.

## 2026-05-14 - Begin Controlled Phase 1

Status: Accepted

Context:

Phase 0 documentation is complete enough to begin a constrained technical validation phase.

Decision:

Begin Phase 1 with only the minimal skeleton, minimal Avalonia foundation, and first playback spike.

Consequences:

- Create only `Auralith.App`, `Auralith.Core`, `Auralith.Playback`, and `Auralith.Playback.Mpv`.
- Do not create `Auralith.Media`, `Auralith.UI`, `Auralith.UI.DesignSystem`, `Auralith.Infrastructure`, audio/video player projects, broad MVVM infrastructure, DI setup, settings, metadata, playlists, themes, tray, or plugin systems.
- Scope remains technical validation, not player buildout.

## 2026-05-14 - Native libmpv Availability Blocks Playback Validation

Status: Accepted

Context:

The first runtime launch found that Hanuman/libmpv requires native `libmpv.2` to be available to the app.

Decision:

Treat native libmpv availability as the immediate blocker for continuing embedded playback validation.

Consequences:

- The app should fail in a controlled way when native libmpv is missing.
- Windows development needs a compatible bundled native libmpv next to the app output before playback can be validated.
- Embedded rendering and playback controls remain unvalidated until native loading is resolved.

## 2026-05-14 - Minimal Testing Foundation

Status: Accepted

Context:

Phase 1 needs regression protection for emerging Core/Playback logic without turning tests into infrastructure.

Decision:

Use a lightweight xUnit v3 + Shouldly test foundation with `Auralith.Core.Tests` and `Auralith.Playback.Tests`.

Consequences:

- Unit tests should cover non-native logic and basic assembly/project integrity.
- Do not add UI automation, screenshot testing, native libmpv integration tests, coverage gates, benchmarks, excessive mocking, or test-only infrastructure at this stage.
- Native playback behavior remains manual/spike validation until libmpv loading and embedded rendering assumptions are validated.

## 2026-05-14 - Phase 1 Dev-Time Native libmpv Loading

Status: Accepted

Context:

The selected Hanuman binding needs a native libmpv runtime before embedded playback can be validated. On Windows, the binding expects `libmpv-2.dll`.

Decision:

For the Phase 1 spike, `Auralith.Playback.Mpv` probes for native libmpv in a small set of development-time locations and configures `MpvApi.RootPath` when found. Windows should use local `libmpv-2.dll` and companion DLLs next to the app output or under `runtimes/win-x64/native`. Linux/Arch may initially rely on system `libmpv.so.2`.

Consequences:

- This is not a full packaging or installer system.
- Native probing remains isolated in `Auralith.Playback.Mpv`.
- Missing native libmpv should produce controlled failure, not an app crash.
- Windows future distribution should still prefer bundled native libmpv so users do not manually install mpv.

## 2026-05-14 - File Input Model For Playback Spike

Status: Accepted

Context:

Auralith needs to open local media through several user paths without introducing playlists, media library behavior, or OS installer work during the spike.

Decision:

Support app-level single-file open requests through the file picker, command-line media path, and single-file drag/drop. Treat OS `Open with Auralith` as two concerns: app capability through command-line arguments now, and OS file association registration later as packaging work.

Consequences:

- `Auralith.Core` owns basic path validation through a small media-open request model.
- The current spike rejects folders, missing paths, invalid paths, and multiple dropped files gracefully.
- No playlist, queue persistence, recent files, metadata extraction, folder scanning, registry changes, or Linux desktop MIME registration is introduced.
