# Design System Notes

This document records design-system intent only. It is not a control library specification and should not trigger UI implementation during the current phase.

## Goals

- Establish a stable visual language early.
- Keep layout structure independent from theme feel.
- Support long-term expansion without visual drift.
- Make audio and video modes feel related.

## Early Tokens To Define Later

Future design work should likely define:

- Color roles rather than raw colors.
- Typography roles.
- Spacing scale.
- Corner radius scale.
- Elevation/material rules.
- Motion durations and easing.
- Icon sizing.
- Focus and keyboard navigation states.
- Density modes.

## Stability Rule

Themes may change visual treatment but must not require different UI trees. Layout, information hierarchy, and navigation structure should remain stable.

## Current Non-Goals

- No Avalonia resource dictionaries yet.
- No custom controls yet.
- No theme files yet.
- No mock UI implementation yet.
