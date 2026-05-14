# Auralith Agent Guide

Auralith is in its foundation phase. The current repository is intentionally documentation-first. Do not create application code unless a later task explicitly changes the project phase.

## Mandatory Read Order

Before making changes, every agent must read:

1. `AGENTS.md`
2. `docs/INDEX.md`
3. `docs/PROJECT_STATE.md`
4. `docs/PROJECT_VISION.md`
5. `docs/ROADMAP.md`
6. `docs/DECISIONS.md`
7. `docs/TODO.md`
8. Any topic document related to the requested work

If the task touches UI direction, also read `docs/UI_PHILOSOPHY.md`, `docs/DESIGN_SYSTEM.md`, and `docs/THEMING_SYSTEM.md`.

If the task touches playback, also read `docs/PLAYBACK_NOTES.md`, `docs/AUDIO_MODE.md`, and `docs/VIDEO_MODE.md`.

## Current Phase Rules

The current phase is documentation and project continuity setup only.

Allowed now:

- Create and maintain Markdown documentation.
- Organize project knowledge for long-running agent work.
- Record confirmed decisions, open questions, roadmap items, and session outcomes.
- Keep the solution file aware of documentation items so Rider shows them.

Not allowed now:

- No UI implementation.
- No windows.
- No playback engine.
- No libmpv integration.
- No service layer.
- No database layer.
- No DI setup.
- No MVVM scaffolding.
- No fake placeholders created only to show progress.
- No production code without an explicit phase change.

## Project Philosophy

Auralith is a modern cross-platform audio/video player for Windows 10/11+ and Linux, with Arch Linux as a first-class target.

The product direction is quiet premium, clean, technical, dark-first, and restrained. It should avoid gamer-style RGB, overloaded VLC-style surfaces, old-OS compromises, and settings sprawl. Minimalism must not become primitive. Advanced behavior should exist, but should be discoverable through context menus and advanced settings instead of dominating the main interface.

Auralith uses one main media shell with adaptive presentation modes. Opening video switches the shell into Video Presentation Mode; opening audio switches it into Audio Presentation Mode. These are not separate players or separate primary windows. Playback state, queue/session state, theme state, and basic transport logic are shared.

Themes may change feel, density, motion, edge treatment, contrast, and material behavior, but not the structural layout of the application.

## Documentation Workflow

Keep documentation useful, current, and separated by confidence level:

- Confirmed decisions go in `docs/DECISIONS.md`.
- Plans and sequencing go in `docs/ROADMAP.md`.
- Current actionable work goes in `docs/TODO.md`.
- Speculative concepts go in `docs/IDEAS.md`.
- Session outcomes go in `docs/SESSION_LOG.md`.
- Stable project constraints go in topic documents.

When adding a new document, update `docs/INDEX.md` and the solution items in `Auralith.sln`.

When changing project direction, update the narrowest relevant document first, then update `docs/PROJECT_STATE.md` if the change affects the current phase or repository status.

## Session Log Rules

Every meaningful session should append an entry to `docs/SESSION_LOG.md` using this shape:

```markdown
## YYYY-MM-DD - Short Title

Context:
- Why the session happened.

Changed:
- What changed in files or decisions.

Notes:
- Anything the next session should know.

Next:
- Concrete follow-up items.
```

Keep entries factual. Do not use the log as a brainstorm area.

## Roadmap Rules

`docs/ROADMAP.md` describes phase order and intent. It is not a dumping ground for every idea.

Update the roadmap when:

- A phase is added, removed, split, or completed.
- A major dependency changes.
- Scope moves between current and future phases.

Do not mark implementation phases as active until the user explicitly authorizes that phase.

## Decision Rules

Use `docs/DECISIONS.md` for decisions that future agents should not relitigate casually.

Decision entries must include:

- Status: Proposed, Accepted, Superseded, or Rejected.
- Date.
- Context.
- Decision.
- Consequences.

If a decision changes, do not erase history. Add a new decision and mark the old one Superseded.

## Idea Rules

Use `docs/IDEAS.md` for useful but uncommitted thoughts. Ideas are not requirements.

Promote an idea only when it becomes:

- A confirmed decision in `docs/DECISIONS.md`.
- A roadmap item in `docs/ROADMAP.md`.
- A concrete task in `docs/TODO.md`.

Do not let speculative ideas leak into architecture notes as if they were settled.

## Change Discipline

Prefer small, explicit documentation changes over broad rewrites. Preserve existing context unless it is wrong or obsolete. If a file has user changes, work with them.

Before implementation work begins in a future phase, agents should create or update planning docs instead of improvising architecture directly in code.

## Completion Checklist

Before ending a session:

- Update `docs/SESSION_LOG.md` if meaningful work occurred.
- Update `docs/TODO.md` when tasks were completed or discovered.
- Update `docs/DECISIONS.md` if a durable decision was made.
- Update `docs/INDEX.md` if documentation files changed.
- Verify no production code was added during documentation-only phases.
- Provide a ready-to-use English commit description in 1-2 sentences summarizing the completed work.
