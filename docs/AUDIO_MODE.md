# Audio Presentation Mode

Audio mode is expected to be richer than video mode because audio playback benefits from metadata, queue, and playlist context.

## Product Intent

Audio Presentation Mode lives inside the unified media shell. It should support a focused listening experience without becoming a full media-library clone too early. It should make current track identity, playback state, queue, and useful metadata easy to understand.

## Future Experience Areas

- Now playing.
- Cover art.
- Title, artist, and album.
- Duration and progress.
- Unified play/pause button.
- Previous/next buttons.
- Playlist and queue surface.
- Repeat/shuffle in a future phase.
- Track metadata.
- Metadata area.
- Context actions.
- Local file metadata through TagLibSharp.
- Future internet metadata providers.

## Design Direction

Audio can carry more visible UI than video, but it should still remain calm and structured. Avoid dense toolbars and permanent advanced controls.

The audio timeline should use the shared `AuralithTimeline` concept, with more visible and persistent styling than video mode. Subtle dynamic background extraction from cover art can be explored later, but it must avoid RGB noise and visual overload.

Metadata enrichment from the internet remains an optional future provider/plugin idea, not MVP scope.

## Current Non-Goals

- No audio presentation implementation.
- No playlist model.
- No metadata database.
- No TagLibSharp integration.
