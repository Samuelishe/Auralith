# Theming System Notes

Themes are intended to modify feel, not structure.

## Theme Philosophy

Auralith may support style themes such as Minimal, Soft, Sharp, Compact, and Floating. These names describe visual treatment and density, not separate layouts.

## Stable Across Themes

Themes must preserve:

- Layout.
- Control tree.
- UX structure.
- Basic interaction model.
- Navigation model.
- Information hierarchy.

## Variable Across Themes

Themes may alter:

- Colors.
- Spacing.
- Radius.
- Stroke thickness.
- Shadows.
- Opacity.
- Animation timings.
- Control density.
- Overlay material feel.
- Timeline thickness and hover scale.
- Button shape and size within safe layout bounds.

## Color Themes And Style Themes

Color themes define palette and contrast.

Style themes define controlled feel and density through tokens. Examples:

- Minimal.
- Soft.
- Sharp.
- Compact.
- Floating.

These must not become AIMP/Winamp-style free-form skins. Auralith uses a controlled token system where layout stability matters more than visual experimentation.

## Future Implementation Direction

When implementation begins, prefer tokenized design roles over raw per-control styling. Avoid creating theme-specific control trees.

## Current Non-Goals

- No Avalonia theme files.
- No resource dictionaries.
- No design-token implementation.
