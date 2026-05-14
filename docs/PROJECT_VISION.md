# Project Vision

Auralith is a modern cross-platform audio/video player for Windows 10/11+ and Linux, with Arch Linux treated as a first-class Linux target.

## Product Identity

Auralith should feel like a quiet, premium desktop application: fast, clean, technically precise, and calm. It should avoid both nostalgic media-player clutter and decorative visual noise.

The goal is not to clone VLC, mpv frontends, Spotify, or YouTube. The goal is a coherent desktop media player that respects local media, metadata, playlists, video playback, subtitles, and modern interaction expectations.

## Design Character

- Dark-first.
- Clean technical aesthetic.
- Soft but restrained motion.
- Minimal without feeling unfinished.
- Premium without gloss.
- Advanced features available without dominating the surface.

## Product Boundaries

Auralith intentionally does not target old operating systems. Compatibility should not drive the product toward outdated UX or obsolete platform assumptions.

The application should support both audio and video through one unified media shell. Audio and video should use adaptive presentation modes, not separate primary player applications or unrelated windows.

## Platform Priority

Auralith remains cross-platform for Windows and Linux. When a feature cannot be implemented with full parity everywhere, polish and fallback priority is:

1. Windows 11.
2. Windows 10.
3. Arch Linux / modern Linux desktop environments.

This priority must not justify careless Windows-only architecture. It may guide feature polish, platform-specific fallbacks, and honest documentation of limitations.

## Long-Term Direction

Long-term ambitions include:

- Local audio and video playback.
- Unified media shell with adaptive Audio and Video Presentation Modes.
- Shared playback session, queue, transport controls, and timeline foundation.
- Metadata-rich audio experience.
- Modern timeline and seeking behavior.
- Direct video surface interactions such as click/tap play-pause and double-click fullscreen.
- Subtitles.
- Audio track switching.
- Audio equalizer, video adjustments, and audio normalization after playback capability validation.
- System tray behavior, especially for audio playback where platform support allows it.
- Extensible internet metadata providers.
- Themable visual feel without structural layout fragmentation.
- Carefully scoped advanced settings.

These ambitions are not permission to implement everything immediately. They define the long arc of the project.
