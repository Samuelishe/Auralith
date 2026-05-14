# Theming System Notes

Themes are intended to modify feel, not structure.

## Theme Philosophy

Auralith may support style themes such as Minimal, Soft, Sharp, Compact, and Floating. These names describe visual treatment and density, not separate layouts.

## Stable Across Themes

Themes must preserve:

- Window structure.
- Navigation model.
- Control placement.
- Information hierarchy.
- Feature availability.

## Variable Across Themes

Themes may alter:

- Color roles.
- Contrast.
- Surface material.
- Corner radius.
- Density.
- Motion feel.
- Border and shadow treatment.

## Future Implementation Direction

When implementation begins, prefer tokenized design roles over raw per-control styling. Avoid creating theme-specific control trees.

## Current Non-Goals

- No Avalonia theme files.
- No resource dictionaries.
- No design-token implementation.
