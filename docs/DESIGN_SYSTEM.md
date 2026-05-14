# Design System Notes

This document records design-system intent only. It is not a control library specification and should not trigger UI implementation during the current phase.

## Goals

- Establish a stable visual language early.
- Keep layout structure independent from theme feel.
- Support long-term expansion without visual drift.
- Make Audio and Video Presentation Modes feel like variants of one shell.

## Early Tokens To Define Later

Future design work should likely define:

- Color tokens.
- Typography tokens.
- Spacing tokens.
- Radius tokens.
- Stroke tokens.
- Elevation/shadow tokens.
- Opacity tokens.
- Animation tokens.
- Density tokens.
- Icon sizing.
- Focus and keyboard navigation states.

## Stability Rule

Themes may change visual treatment but must not require different UI trees. Layout, information hierarchy, and navigation structure should remain stable.

## Future Shared UI Controls

These are planning names, not code to create now:

- `AuralithTimeline`
- `TransportBar`
- `OverlayControlsBar`
- `MediaPresentationHost`
- `IconButton` / `TransportButton`
- `VolumeSlider`
- `MetadataPanel`
- `QueuePanel`
- `CoverArtView`

## Current Non-Goals

- No Avalonia resource dictionaries yet.
- No custom controls yet.
- No theme files yet.
- No mock UI implementation yet.
