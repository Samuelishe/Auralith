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

Status: Accepted

Context:

Audio and video playback have different interaction and information-density needs.

Decision:

Audio and video should use separate windows while sharing a single visual language.

Consequences:

- Audio can support richer metadata and playlist surfaces.
- Video can keep content central with minimal disappearing controls.

## 2026-05-14 - Themes Modify Feel, Not Structure

Status: Accepted

Context:

The project may support multiple style themes, but theme flexibility can easily damage maintainability.

Decision:

Themes may alter visual feel, density, motion, contrast, and material treatment, but must not change window structure or information architecture.

Consequences:

- Theme design must be token-driven and layout-stable.
- No skin system that replaces UI structure.

## 2026-05-14 - libmpv Playback Backend

Status: Accepted

Context:

Auralith needs mature audio/video playback with FFmpeg support.

Decision:

Use libmpv as the playback backend, with FFmpeg available through libmpv.

Consequences:

- Future playback work must account for libmpv lifecycle and platform packaging.
- The application should avoid reimplementing codec/playback responsibilities.
