# Playback Notes

Playback direction is known, but playback architecture is not implemented yet.

## Confirmed Direction

- libmpv is the playback backend.
- FFmpeg support comes through libmpv.
- Auralith should not reimplement decoding or media playback primitives.
- Subtitles and audio track switching are important product goals.
- Playback Queue is the primary runtime model.
- Playlists may feed the queue, but queue owns next/previous, shuffle, and history behavior.
- Metadata loading must not block playback: open file, start playback quickly, then enrich metadata asynchronously.

## Thin Playback Abstraction

The abstraction over libmpv should stay practical and narrow. Avoid a large speculative `IUniversalMediaEngine`.

Expected future surface:

- Play, pause, stop.
- Seek.
- Volume.
- Duration and position.
- Media state.
- Track lists.
- Subtitle selection.
- Audio track selection.
- Events/properties mapped from libmpv where useful.

## Future Concerns

- libmpv lifecycle ownership.
- Cross-platform native library loading.
- Packaging for Windows and Linux.
- Mapping mpv events into application state.
- Error reporting and recoverability.
- Subtitle discovery, selection, and styling boundaries.
- Audio track enumeration and switching.
- Timeline precision and buffering state.
- Queue/session ownership.
- Async metadata enrichment.

## Non-Goals For Current Phase

- No playback wrapper.
- No mpv binding choice.
- No playback service.
- No universal media engine abstraction.
- No test media fixtures.
- No native dependency packaging.
