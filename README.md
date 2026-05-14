# Auralith

Auralith is an early-stage modern cross-platform audio/video player project built with C#/.NET 10, Avalonia UI, and a libmpv/FFmpeg playback direction.

The project is not feature-complete and is not production-ready. It is currently in a controlled Phase 1 technical validation stage: minimal project skeleton, minimal Avalonia shell, and a constrained playback spike focused on validating Avalonia + libmpv integration.

## Current Status

Auralith currently has:

- A documentation-first project foundation.
- A minimal .NET 10 solution skeleton.
- A minimal Avalonia desktop application shell.
- A unified main media shell direction.
- A narrow playback spike boundary using `HanumanInstitute.LibMpv` / `HanumanInstitute.LibMpv.Avalonia`.
- A minimal `Auralith.Playback` contract and `Auralith.Playback.Mpv` implementation boundary.

Validated so far:

- `dotnet restore Auralith.sln` succeeds.
- `dotnet build Auralith.sln --no-restore` succeeds.
- The app starts in a controlled failure mode when native libmpv is missing.
- Concrete Hanuman/libmpv usage is isolated in `Auralith.Playback.Mpv`.

Not yet validated:

- Embedded live video playback.
- Overlay z-order over active video.
- Runtime seek/volume/timeline behavior with real media.
- Fullscreen behavior with active video rendering.
- Native libmpv packaging strategy.

Current blocker:

- On Windows, the spike needs a compatible native `libmpv.2` available in the app output/runtime path. Until that is supplied, embedded playback cannot be validated.

## Philosophy

Auralith is intended to be a quiet, modern desktop media player:

- Clean, restrained UI.
- No overloaded VLC-style control surface.
- No chaotic skin system.
- No gamer-RGB visual direction.
- Minimalism without primitive UX.
- Advanced features hidden until contextually needed.
- Architecture shaped by real vertical slices, not speculative framework-building.

The design direction is dark-first, calm, technical, and long-lived. Themes should modify feel, not structure.

## Product Direction

Auralith is planned around a unified media shell with adaptive presentation modes:

- `Video Presentation Mode` for content-first playback with minimal overlay controls.
- `Audio Presentation Mode` for a richer metadata and queue-oriented experience.

These are not separate applications. Auralith should not become `Auralith.AudioPlayer` plus `Auralith.VideoPlayer`. Playback/session/control foundations should remain shared.

Planned UI direction:

- Minimal overlay-driven video controls.
- Single-click/tap play-pause and double-click fullscreen for video.
- Shared timeline concept with mode-specific styling.
- Richer future audio view with cover art, metadata, and queue/playlist surfaces.
- Token-driven theming where themes change colors, density, motion, material feel, and control styling without changing layout structure.

## Technology Stack

Current stack:

- C#
- .NET 10
- Avalonia UI
- libmpv playback direction through HanumanInstitute bindings

Planned or future stack areas:

- FFmpeg through libmpv
- SQLite for local state where needed
- TagLibSharp for local media metadata
- Serilog for structured logging
- Microsoft.Extensions.DependencyInjection only when implementation pressure justifies it

## Playback Direction

The current primary playback spike candidate is:

- `HanumanInstitute.LibMpv`
- `HanumanInstitute.LibMpv.Avalonia`

Fallback:

- `MPVSharp`

Last resort:

- Manual P/Invoke over libmpv, only if existing bindings fail badly

Avoided as initial direction:

- `Mpv.NET`
- `LibVLCSharp`
- Avalonia Pro `MediaPlayer`
- Large custom playback engine before validation

The project intentionally chose a libmpv direction and should not drift toward VLC-centric architecture unless libmpv validation fails badly.

## Platform Targets

Target platforms:

- Windows 11
- Windows 10
- Arch Linux / modern Linux desktop environments

Platform polish priority when full parity is not possible:

1. Windows 11
2. Windows 10
3. Arch Linux / modern Linux desktops

This priority should not lead to careless Windows-only architecture. It can guide platform-specific polish and fallback behavior.

## Building

Requirements:

- .NET SDK 10
- Network access for NuGet restore unless packages are already cached

Restore and build:

```powershell
dotnet restore Auralith.sln
dotnet build Auralith.sln --no-restore
```

Current expected build result:

- The solution builds successfully on .NET 10.

## Running

Run the current minimal app:

```powershell
dotnet run --project src/Auralith.App/Auralith.App.csproj
```

Important: the app is not a usable media player yet. At the current stage it is a technical spike shell.

If native libmpv is missing, the app should start and show a controlled failure message instead of crashing.

## Native Dependencies

### Windows

Current direction:

- Auralith should eventually bundle native libmpv with the app.
- Users should not need to manually install mpv.
- Windows 11 is the highest polish target.

Current development-stage blocker:

- The playback spike needs a compatible `libmpv.2` native library available to the app output/runtime path.
- Packaging/loading strategy is not finalized.

### Linux / Arch Linux

Current direction:

- Early development may rely on system libmpv installed through the package manager.
- Arch Linux is treated as a first-class Linux target.
- Future AppImage/Flatpak or bundled approaches may be investigated later.

Preliminary Arch-style dependency expectation:

```bash
sudo pacman -S dotnet-sdk mpv
```

This command is preliminary. Exact Linux development and packaging instructions are not finalized yet.

## Repository Structure

Current structure:

```text
AGENTS.md
README.md
Auralith.sln
Directory.Packages.props
docs/
src/
  Auralith.App/
  Auralith.Core/
  Auralith.Playback/
  Auralith.Playback.Mpv/
```

Not currently present:

- `Auralith.Media`
- `Auralith.UI`
- `Auralith.UI.DesignSystem`
- `Auralith.Infrastructure`
- `Auralith.AudioPlayer`
- `Auralith.VideoPlayer`
- test projects

Those projects should not be created until real implementation pressure justifies them.

## Development Philosophy

Auralith intentionally avoids:

- Enterprise architecture theatre.
- Speculative abstraction layers.
- Fake placeholder services.
- Premature MVVM scaffolding.
- Backend-agnostic playback architecture before evidence exists.
- Empty semantic projects for appearance.
- Broad architecture expansion for future ideas.

Preferred approach:

- Small vertical slices.
- Thin practical boundaries.
- Technical validation before design certainty.
- Documentation of assumptions, failures, and architectural pressure.
- Shared shell and playback concepts rather than separate audio/video applications.

## Documentation

Documentation is part of the development workflow, not an afterthought.

Start here:

- [`AGENTS.md`](AGENTS.md) - Agent/Codex workflow and rules.
- [`docs/INDEX.md`](docs/INDEX.md) - Documentation map.
- [`docs/PROJECT_STATE.md`](docs/PROJECT_STATE.md) - Current project state.
- [`docs/ROADMAP.md`](docs/ROADMAP.md) - Phase roadmap.
- [`docs/DECISIONS.md`](docs/DECISIONS.md) - Accepted/rejected/superseded decisions.
- [`docs/SESSION_LOG.md`](docs/SESSION_LOG.md) - Session history.
- [`docs/PLAYBACK_NOTES.md`](docs/PLAYBACK_NOTES.md) - Playback spike notes and known blockers.

Future contributors and LLM/Codex sessions should read `AGENTS.md` before making changes.

## Open-Source Acknowledgements

Auralith depends on, or plans to depend on, several external open-source technologies. These projects are not hidden behind Auralith branding.

- [.NET](https://dotnet.microsoft.com/) - Microsoft, .NET Foundation. Runtime and SDK.
- [Avalonia UI](https://avaloniaui.net/) - Avalonia UI project. Cross-platform .NET UI framework.
- [mpv / libmpv](https://mpv.io/) - mpv project. Media playback engine and client API.
- [FFmpeg](https://ffmpeg.org/) - FFmpeg project. Multimedia framework used by mpv/libmpv.
- [HanumanInstitute.LibMpv](https://github.com/mysteryx93/LibMpv-OpenGL) - Hanuman Institute / mysteryx93. .NET libmpv bindings and Avalonia integration used for the current spike.
- [TagLibSharp](https://github.com/mono/taglib-sharp) - Planned metadata reading library.
- [Serilog](https://serilog.net/) - Planned structured logging library.
- [SQLite](https://www.sqlite.org/) - Planned embedded database for local state where needed.

License obligations for all third-party components must be reviewed before distribution.

## Contributing

The project is not ready for broad feature contributions yet. The most useful contributions at this stage are:

- Reproducing the playback spike on supported platforms.
- Validating native libmpv loading.
- Improving documentation around real findings.
- Keeping scope constrained to the current phase.

Before contributing:

1. Read `AGENTS.md`.
2. Read the relevant files in `docs/`.
3. Avoid creating speculative layers or placeholder architecture.
4. Record important findings in `docs/SESSION_LOG.md`, `docs/TODO.md`, and `docs/DECISIONS.md` where appropriate.

## License

No project license has been selected yet.

Do not assume distribution rights or final licensing terms until a license is explicitly added to the repository.
