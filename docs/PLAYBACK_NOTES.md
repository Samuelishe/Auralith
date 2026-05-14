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
- Audio equalizer support through mpv/FFmpeg filters.
- Audio normalization, replaygain, loudness normalization, dynamic range compression, and dialogue clarity options.
- Video adjustment support for brightness, contrast, saturation, gamma, and possibly sharpness.
- Clear UX boundaries for processing controls so they do not become settings clutter.

## Media Processing Planning

Audio and video processing features are planned but spike-dependent.

Audio processing should investigate:

- Equalizer support.
- ReplayGain or loudness normalization for music.
- Dynamic range compression or dialogue clarity for video.
- mpv/FFmpeg filter availability and cross-platform behavior.

Video processing should investigate:

- Brightness.
- Contrast.
- Saturation.
- Gamma.
- Sharpness if backend support is practical.

Do not implement processing controls before the libmpv playback spike confirms capabilities, performance implications, and UX boundaries.

## Non-Goals For Current Phase

- No playback wrapper.
- No mpv binding choice.
- No playback service.
- No universal media engine abstraction.
- No test media fixtures.
- No native dependency packaging.
- No equalizer, normalization, dynamic range, or video adjustment implementation.
