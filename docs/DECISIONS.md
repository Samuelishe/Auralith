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
