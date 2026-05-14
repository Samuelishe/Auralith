# UI Philosophy

Auralith should be visually quiet, modern, and efficient. The UI should support repeated daily use rather than impress only in screenshots.

## Principles

- Keep the main surfaces calm and scannable.
- Prefer direct manipulation where it fits media playback.
- Hide advanced controls until they are contextually relevant.
- Use one unified media shell with adaptive presentation modes.
- Reuse shared transport controls, shared playback session state, and a shared timeline foundation.
- Avoid layout changes between themes.
- Avoid settings pages that expose every internal option as a user concern.

## Anti-Goals

- No gamer RGB styling.
- No VLC-style control overload.
- No skin system that lets themes rebuild the application layout.
- No decorative complexity pretending to be power.
- No implementation of UI during documentation-only phases.

## Unified Media Shell

Auralith should not behave like two separate players. `MainWindow` or the main media shell is the single primary interaction point. Opening video switches the shell into Video Presentation Mode. Opening audio switches it into Audio Presentation Mode.

Playback state, queue/session state, theme state, and base control logic remain shared. Audio and video UI should differ only where content type requires it.

Shared concepts:

- Shared transport controls.
- Shared playback session.
- Shared queue model.
- Shared timeline control with mode-specific styling.
- Shared visual language.

## Presentation Modes

Audio Presentation Mode may expose richer information: metadata, playlists, queue, album/track context, and discovery surfaces.

Video Presentation Mode should protect the content. Controls are overlays, minimal by default, and should appear on mouse movement/hover and disappear when idle.

## Direct Video Interaction

Video should support direct surface interactions inspired by modern web players:

- Single click or tap toggles play/pause.
- Double click toggles fullscreen.
- Future exploration: hold on the right side to accelerate forward seek, and hold on the left side to accelerate backward seek.

These interactions must be designed so they do not conflict with subtitle selection, right-click context menus, drag/window move behavior, overlay controls, or text selection.

## Timeline Direction

The playback timeline should feel modern and web-literate, closer to contemporary YouTube/web-player expectations than legacy desktop sliders. This includes strong hover affordances, accurate seeking, and readable buffered/progress states when appropriate.
