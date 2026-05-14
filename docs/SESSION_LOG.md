# Session Log

## 2026-05-14 - Documentation Foundation

Context:

- Initial repository existed with a solution file and no application code.
- The user requested documentation and organizational groundwork only.

Changed:

- Added `AGENTS.md` as the main LLM/Codex onboarding and workflow file.
- Added `docs` with index, vision, state, roadmap, decisions, TODO, and topic notes.
- Added documentation files as solution items in `Auralith.sln` for Rider visibility.

Notes:

- Phase 0 is active.
- Production code remains out of scope.
- Confirmed decisions are separated from speculative ideas.

Next:

- Project owner should review the documentation foundation.
- Do not begin Phase 1 until explicitly approved.

## 2026-05-14 - Ignore Rules And Commit Summary Rule

Context:

- The repository needed ignore rules appropriate for future .NET/Avalonia development.
- The project owner requested a standing rule that each agent session ends with a short English commit description.

Changed:

- Expanded `.gitignore` for build output, IDE state, NuGet artifacts, test output, logs, and OS files.
- Added the commit description requirement to `AGENTS.md`.

Notes:

- `.idea/` is intentionally ignored and should not be committed.

Next:

- Keep future commits limited to source, documentation, and intentional project files.

## 2026-05-14 - Unified Media Shell Model

Context:

- The project owner clarified that Auralith should not be built as separate audio and video player windows or projects.
- The project is still in documentation/concept planning only.

Changed:

- Updated product, UI, architecture, playback, theming, roadmap, TODO, and decision docs around a unified media shell.
- Accepted adaptive Audio and Video Presentation Modes as the primary model.
- Recorded queue-first playback, non-blocking metadata enrichment, and thin libmpv abstraction guidance.

Notes:

- `MainWindow`/main media shell is the planned primary interaction point.
- Audio and video presentation share playback/session/control foundations.
- Planned Phase 1 solution structure remains a planning direction only and has not been created.

Next:

- Review and finalize Phase 1 solution structure before creating any projects.
- Define the first vertical slice and playback spike questions.

## 2026-05-14 - Video Interaction, Processing, And Tray Planning

Context:

- The project owner added requirements for video surface interaction, future media processing, tray behavior, and platform priority.
- The project remains in documentation/concept planning only.

Changed:

- Documented single click/tap play-pause and double-click fullscreen for video.
- Added future hold-to-seek exploration and conflict rules for video interactions.
- Added planning notes for audio equalizer, video adjustments, audio normalization, dynamic range compression, and dialogue clarity.
- Added tray behavior expectations and platform priority guidance.

Notes:

- Processing features must wait for libmpv playback spike validation.
- Tray behavior must be documented honestly where Linux desktop environment support varies.

Next:

- Investigate libmpv audio/video processing capabilities.
- Investigate Avalonia/system tray cross-platform options.
- Define video interaction conflict rules before UI implementation.

## 2026-05-14 - Playback Integration And Phase 1 Planning

Context:

- The project owner refined playback integration strategy and Phase 1 planning boundaries.
- The project remains documentation/planning only.

Changed:

- Documented `HanumanInstitute.LibMpv` and `HanumanInstitute.LibMpv.Avalonia` as primary playback spike candidates.
- Added fallback/avoid lists for playback integration options.
- Added dependency isolation rules so UI layers do not depend directly on concrete libmpv APIs.
- Added playback spike acceptance criteria, native packaging direction, and Phase 1 planning constraints.
- Recorded native packaging risk and vertical-slice-first architecture guidance.

Notes:

- No dependencies, projects, placeholders, or production code should be created in Phase 0.
- Playback architecture must remain thin and evidence-driven until the spike validates assumptions.

Next:

- Finalize playback spike acceptance criteria.
- Define native libmpv loading and packaging strategy for Windows and Linux.
- Define shell, overlay, transport, timeline, fullscreen, tray, and interaction ownership boundaries.

## 2026-05-14 - Phase 1 Planning Decisions

Context:

- The project owner approved specific Phase 1 planning decisions while keeping the repository documentation-only.
- No implementation, packages, projects, or placeholders were requested.

Changed:

- Fixed playback binding strategy around `HanumanInstitute.LibMpv` / `HanumanInstitute.LibMpv.Avalonia`, with `MPVSharp` as fallback and manual P/Invoke as last resort.
- Recorded the planned solution structure and minimal first skeleton guidance.
- Accepted the video-first vertical slice recommendation.
- Added preliminary ownership model for `MainWindow`, `MediaShell`, `PlaybackSession`, `PlaybackQueue`, and `VideoPresentation`.
- Confirmed shared `AuralithTimeline`, tray deferral, packaging direction, and processing-feature validation boundaries.

Notes:

- Phase 0 is close to completion, but implementation has not started.
- Phase 1 may begin only after explicit user approval.

Next:

- Review Phase 1 readiness.
- Keep first implementation limited to minimal skeleton and the recommended video-first slice when approved.

## 2026-05-14 - Phase 1 Minimal Skeleton And Playback Spike Start

Context:

- The project owner approved beginning Phase 1 in a controlled, intentionally minimal form.
- The approved scope was minimal skeleton, minimal Avalonia app foundation, and constrained libmpv playback spike.

Changed:

- Created only `src/Auralith.App`, `src/Auralith.Core`, `src/Auralith.Playback`, and `src/Auralith.Playback.Mpv`.
- Added minimal Avalonia startup and unified main media shell.
- Added minimal `IPlaybackSession` based on the approved slice.
- Added `MpvPlaybackSession` and `MpvPlaybackSurface` in `Auralith.Playback.Mpv`.
- Kept Hanuman/libmpv binding references out of `Auralith.App`.
- Added overlay controls, timeline, play/pause, stop, volume, fullscreen, single-click, double-click, and right-click placeholder handling for the spike.

Validated:

- `dotnet restore Auralith.sln` succeeds.
- `dotnet build Auralith.sln --no-restore` succeeds with 0 warnings and 0 errors.
- GUI startup no longer crashes when native libmpv is missing; the app starts in a controlled failure mode.

Failed / Blocked:

- Native `libmpv.2` is not available in the Windows app output/runtime path.
- Actual embedded playback, overlay z-order over live video, seek, volume, timeline sync, fullscreen-with-video, and live input behavior are not yet validated.

Assumptions Updated:

- Native dependency loading is an immediate Phase 1 blocker, not only later packaging work.

Next:

- Supply or define a compatible Windows native libmpv strategy for local development.
- Re-run the video-first playback spike with native libmpv available.

## 2026-05-14 - Repository README

Context:

- The project needed a GitHub-facing README that reflects the current real state rather than future ambitions as completed features.

Changed:

- Added root `README.md`.
- Documented current Phase 1 status, build/run instructions, native libmpv blocker, repository structure, development philosophy, and third-party technology acknowledgements.
- Added `README.md` to solution items for Rider visibility.

Notes:

- README explicitly states that Auralith is not production-ready and embedded playback is not yet validated.

Next:

- Update README when native libmpv loading and embedded playback are validated.

## 2026-05-14 - Minimal Testing Foundation

Context:

- Phase 1 needed a restrained test harness to protect current non-native Core/Playback logic without expanding architecture.

Changed:

- Added `tests/Auralith.Core.Tests` using xUnit v3 and Shouldly.
- Added `tests/Auralith.Playback.Tests` using xUnit v3 and Shouldly.
- Added `PlaybackConstraints` in `Auralith.Playback` and used it from `MpvPlaybackSession`.
- Added small tests for assembly loading and playback volume/position constraints.
- Added test projects to the solution.

Validated:

- `dotnet restore Auralith.sln` succeeds.
- `dotnet build Auralith.sln --no-restore` succeeds with 0 warnings and 0 errors.
- `dotnet test Auralith.sln` succeeds: 9 tests passed.

Notes:

- UI automation, native libmpv integration tests, screenshot tests, coverage gates, and benchmarking remain intentionally out of scope.
- Real playback validation remains manual/spike work until native libmpv loading is resolved.

Next:

- Keep tests focused on non-native logic until implementation pressure justifies broader coverage.

## 2026-05-14 - Native libmpv Loading Strategy And File Input

Context:

- Phase 1 remained constrained to playback spike work.
- The immediate blocker was native libmpv runtime/loading on Windows.
- The project also needed a clear app-level model for future file opening scenarios.

Changed:

- Identified the selected Hanuman binding's native names: `libmpv-2.dll` on Windows and `libmpv.so.2` on Linux.
- Added development-time native probing in `Auralith.Playback.Mpv` and configured `MpvApi.RootPath` when a native runtime is found.
- Kept missing native libmpv as a controlled failure instead of an app crash.
- Added `MediaOpenRequest` in `Auralith.Core` for basic local file path validation.
- Added command-line media path support for the app capability side of future `Open with`.
- Added minimal single-file drag/drop support.
- Kept file picker opening on the same validated media-open path.
- Added unit tests for media-open request validation and command-line path selection.

Validated:

- `dotnet restore Auralith.sln` succeeds.
- `dotnet build Auralith.sln --no-restore` succeeds with 0 warnings and 0 errors.
- `dotnet test Auralith.sln` succeeds.

Failed / Blocked:

- No compatible local `libmpv-2.dll` runtime was found, so actual embedded playback is still unvalidated.
- Overlay z-order over live video, real seek/volume behavior, fullscreen with active video, and live input behavior remain manual validation tasks.

Next:

- Provide compatible Windows `libmpv-2.dll` and companion DLLs next to app output or under `runtimes/win-x64/native`.
- Re-run the playback spike with a local media file outside the repository.
- Keep OS file association registration as future packaging work.

## 2026-05-14 - Line Endings And Runtime Developer Experience

Context:

- Windows Git line-ending warnings were creating avoidable noise.
- The native libmpv runtime blocker needed clearer developer-facing documentation and failure messaging.

Changed:

- Added root `.gitattributes` to define predictable line endings.
- Kept most text files normalized to LF and `.sln` files as CRLF for Visual Studio/Rider compatibility.
- Marked binary assets, native libraries, executables, and media files as binary.
- Improved the missing native libmpv message for Windows and Linux.
- Clarified that Windows Phase 1 development requires a compatible `libmpv-2.dll` runtime placed locally, while future Windows releases should bundle native libmpv.
- Documented why native DLLs are not committed yet.

Validated:

- Build and test verification should be run after this documentation/runtime clarity update.

Failed / Blocked:

- Embedded playback remains blocked until a compatible Windows `libmpv-2.dll` and companion DLLs are supplied.

Next:

- Validate the improved missing-runtime message through a manual app launch.
- Review licensing and packaging requirements before any native binaries are added to repository or release artifacts.

## 2026-05-14 - Dev-Time libmpv Setup And Local Launch Validation

Context:

- Phase 1 continued strictly inside native runtime validation and playback spike work.
- Ordinary Windows users should eventually receive a bundled runtime, but local development needed one-command native setup.

Changed:

- Added `tools/setup-libmpv-windows.ps1`.
- Added `runtimes/win-x64/native/README.md` and `.gitkeep`.
- Updated `.gitignore` so local native DLLs, PDBs, archives, and `.auralith/` download/extract files are not committed.
- Added a minimal Windows application manifest to `Auralith.App` after Avalonia `NativeControlHost` required a supported OS declaration.

Validated:

- The dev helper downloaded shinchiro `mpv-dev-x86_64-20260421-git-5921fe5.7z`.
- The helper found `libmpv-2.dll` and copied it to `runtimes/win-x64/native`.
- Initial launch with local video failed before the manifest with a Windows `NativeControlHost` child-window error.
- After adding the manifest, Auralith launched with a local video path from `E:\Downloads\Films` and stayed alive for 15 seconds before being stopped manually.

Failed / Blocked:

- Visual embedded playback rendering was not confirmed by Codex.
- Overlay z-order, seek, volume, fullscreen, resize behavior, and drag/drop with real playback still require manual visual validation.

Next:

- Manually run the app with the same local video and confirm rendering and controls.
- Keep native DLLs and downloaded archives out of git.
- Continue treating release packaging as future work, not part of the current spike.
