# UI Philosophy

Auralith should be visually quiet, modern, and efficient. The UI should support repeated daily use rather than impress only in screenshots.

## Principles

- Keep the main surfaces calm and scannable.
- Prefer direct manipulation where it fits media playback.
- Hide advanced controls until they are contextually relevant.
- Keep audio and video visually related but structurally distinct.
- Avoid layout changes between themes.
- Avoid settings pages that expose every internal option as a user concern.

## Anti-Goals

- No gamer RGB styling.
- No VLC-style control overload.
- No skin system that lets themes rebuild the application layout.
- No decorative complexity pretending to be power.
- No implementation of UI during documentation-only phases.

## Audio And Video Split

Audio mode may expose richer information: metadata, playlists, queue, album/track context, and discovery surfaces.

Video mode should protect the content. Controls are overlays, minimal by default, and should disappear when inactive.

## Timeline Direction

The playback timeline should feel modern and web-literate, closer to contemporary YouTube/web-player expectations than legacy desktop sliders. This includes strong hover affordances, accurate seeking, and readable buffered/progress states when appropriate.
