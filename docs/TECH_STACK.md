# Tech Stack

This document records the intended stack. It does not imply dependencies should be installed during the documentation phase.

## Language And Runtime

- C#
- .NET 10

## UI

- Avalonia UI
- MVVM

## Playback

- libmpv
- FFmpeg through libmpv
- `HanumanInstitute.LibMpv`
- `HanumanInstitute.LibMpv.Avalonia`

Current native loading expectations for the Phase 1 spike:

- Windows: `libmpv-2.dll`.
- Linux: `libmpv.so.2`.

The Hanuman binding is the current spike candidate, not an irreversible dependency commitment.

## Data And Metadata

- SQLite
- TagLibSharp

## Infrastructure

- Serilog
- `Microsoft.Extensions.DependencyInjection`

## Platform Targets

- Windows 10/11+
- Linux
- Arch Linux as a first-class Linux target

## Current Constraint

Phase 1 is active only as a constrained technical validation phase. Do not add broad architecture, production systems, or new dependency areas outside the approved playback spike.
