# Playback Notes

Playback direction is known, but playback architecture is not implemented yet.

## Confirmed Direction

- libmpv is the playback backend.
- FFmpeg support comes through libmpv.
- Auralith should not reimplement decoding or media playback primitives.
- Subtitles and audio track switching are important product goals.

## Future Concerns

- libmpv lifecycle ownership.
- Cross-platform native library loading.
- Packaging for Windows and Linux.
- Mapping mpv events into application state.
- Error reporting and recoverability.
- Subtitle discovery, selection, and styling boundaries.
- Audio track enumeration and switching.
- Timeline precision and buffering state.

## Non-Goals For Current Phase

- No playback wrapper.
- No mpv binding choice.
- No playback service.
- No test media fixtures.
- No native dependency packaging.
