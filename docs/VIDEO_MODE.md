# Video Presentation Mode

Video mode should prioritize the media content. UI should appear when useful and disappear when not needed.

## Product Intent

Video Presentation Mode lives inside the unified media shell. It should feel modern and unobtrusive. It should support essential playback controls, subtitles, audio track switching, and timeline interaction without turning into a control dashboard.

## Future Experience Areas

- Minimal overlay controls.
- Overlay controls that appear on mouse move/hover and disappear when idle.
- Modern timeline interaction.
- Subtitle selection.
- Audio track selection.
- External subtitles.
- Playback speed.
- Aspect ratio and video fit options.
- Fullscreen behavior.
- Borderless behavior.
- Context menu for advanced actions.

## Design Direction

The video surface should avoid permanent chrome where possible. Controls should be readable, precise, and predictable, with careful behavior around mouse movement, keyboard focus, and fullscreen state.

The timeline should use the shared `AuralithTimeline` concept with video-specific styling: thin in idle state, more visible on hover, with seek thumb and active region appearing carefully. Chapter markers and preview thumbnails are future ideas, not MVP requirements.

Advanced video actions should be available through a right-click context menu rather than permanent controls.

## Current Non-Goals

- No video presentation implementation.
- No overlays.
- No fullscreen behavior.
- No subtitle implementation.
