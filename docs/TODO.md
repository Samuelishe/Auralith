# TODO

## Completed Phase 0 / Active Phase 1

- [x] Create documentation entry point.
- [x] Create documentation index.
- [x] Record initial project state.
- [x] Record project vision.
- [x] Record initial decisions.
- [x] Record phase roadmap.
- [x] Add documentation items to the solution for Rider visibility.
- [x] Add repository-level README that reflects current Phase 1 state.
- [x] Configure repository ignore rules for the project foundation.
- [x] Add repository `.gitattributes` line-ending policy.
- [x] Add agent rule for final English commit descriptions.
- [ ] Review documentation with the project owner.
- [x] Decide when Phase 1 may begin.
- [x] Review and finalize Phase 1 solution structure before creating projects.
- [x] Define the first vertical slice.
- [ ] Define Avalonia project creation strategy.
- [ ] Define dependency policy for native libmpv distribution on Windows/Linux.
- [ ] Define initial UI token taxonomy.
- [ ] Define playback spike questions.
- [ ] Finalize playback spike acceptance criteria.
- [x] Define Phase 1 dev-time native libmpv loading strategy.
- [x] Add dev-only Windows libmpv setup helper.
- [ ] Define Windows packaging expectations.
- [ ] Review licensing and distribution requirements before committing bundled native libmpv binaries.
- [ ] Define Arch Linux system dependency expectations.
- [ ] Define shell ownership boundaries.
- [ ] Define MainWindow, MediaShell, PlaybackSession, PlaybackQueue, and VideoPresentation ownership boundaries.
- [ ] Define overlay layering strategy.
- [ ] Define future vertical slice boundaries.
- [ ] Define minimal first skeleton scope: App/Core/Playback/Playback.Mpv only.
- [x] Define minimal playback abstraction surface.
- [ ] Investigate Avalonia/mpv overlay compatibility risks.
- [x] Diagnose playback surface readiness lifecycle.
- [ ] Investigate libmpv support for audio filters/equalizer/normalization.
- [ ] Investigate libmpv support for video adjustments.
- [ ] Investigate Avalonia/system tray cross-platform options.
- [ ] Define click/double-click/hold interaction model for video surface.
- [ ] Define conflict rules for video interactions: context menu, fullscreen, drag, overlay controls, subtitle selection, and text selection.

## Active Spike Follow-Up

- [x] Create minimal `src/Auralith.App`, `src/Auralith.Core`, `src/Auralith.Playback`, and `src/Auralith.Playback.Mpv`.
- [x] Add minimal Avalonia startup and main media shell.
- [x] Isolate Hanuman/libmpv surface code inside `Auralith.Playback.Mpv`.
- [x] Build solution successfully on .NET 10.
- [x] Add minimal xUnit v3 + Shouldly test foundation.
- [x] Verify `dotnet test Auralith.sln`.
- [x] Confirm controlled startup when native libmpv is missing.
- [x] Add command-line file path handling for future `Open with` app capability.
- [x] Add minimal single-file drag/drop handling for the current spike.
- [x] Keep file picker/open command working through the same validated media-open request path.
- [x] Provide compatible Windows native `libmpv-2.dll` locally under `runtimes/win-x64/native` for spike validation.
- [ ] Validate that the improved missing-runtime message is clear during a manual app launch.
- [x] Add Windows application manifest required by Avalonia NativeControlHost.
- [x] Validate embedded video rendering visually.
- [x] Move Phase 1 controls out of native video overlay into a stable bottom control bar.
- [ ] Investigate true overlay rendering over native video surface in a separate future spike.
- [x] Revert OpenGL renderer after black-video regression.
- [x] Disable timeline hover height mutation that caused layout jitter.
- [ ] Validate seek after adding the diagnostic command fallback chain.
- [x] Add seek polling grace period and follow-up seek diagnostics.
- [x] Add pending seek UI state so polling does not immediately snap the slider back before mpv confirms or times out.
- [x] Add visible temporary Phase 1 seek diagnostics and a `+60s` debug seek action.
- [x] Add minimal fullscreen mode that hides header and keeps controls visible.
- [x] Validate command-line pending media reaches libmpv `LoadFile` command dispatch.
- [ ] Validate play/pause, seek, volume, duration/position updates visually.
- [ ] Validate fullscreen and resize behavior.
- [ ] Validate single-click, double-click, and right-click behavior with live video.
- [x] Validate command-line file opening reaches playback session with real native libmpv runtime.
- [ ] Validate drag/drop file opening with real native libmpv playback.
- [ ] Define future OS file association packaging plan for Windows `Open with`.
- [ ] Define future Linux `.desktop` MIME association packaging plan.

## Future

- [ ] Define exact .NET project layout after Phase 1 is approved.
- [ ] Choose dependency versioning strategy.
- [ ] Define coding style and formatting rules.
- [ ] Define build/test verification commands.
- [ ] Investigate libmpv packaging options per OS.
- [ ] Define first UI design-token draft.
- [ ] Draft platform-specific feature capability matrix.
