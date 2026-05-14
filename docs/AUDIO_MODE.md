# Audio Mode

Audio mode is expected to be richer than video mode because audio playback benefits from metadata, queue, and playlist context.

## Product Intent

The audio window should support a focused listening experience without becoming a full media-library clone too early. It should make current track identity, playback state, queue, and useful metadata easy to understand.

## Future Experience Areas

- Now playing.
- Playlist and queue.
- Track metadata.
- Album art.
- Context actions.
- Local file metadata through TagLibSharp.
- Future internet metadata providers.

## Design Direction

Audio can carry more visible UI than video, but it should still remain calm and structured. Avoid dense toolbars and permanent advanced controls.

## Current Non-Goals

- No audio window implementation.
- No playlist model.
- No metadata database.
- No TagLibSharp integration.
