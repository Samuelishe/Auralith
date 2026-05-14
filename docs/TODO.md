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
- [ ] Provide compatible Windows native `libmpv-2.dll` and companion DLLs beside app output or under `runtimes/win-x64/native` for local spike validation.
- [ ] Validate that the improved missing-runtime message is clear during a manual app launch.
- [ ] Validate embedded video rendering.
- [ ] Validate overlay z-order above video.
- [ ] Validate play/pause, seek, volume, duration/position updates.
- [ ] Validate fullscreen and resize behavior.
- [ ] Validate single-click, double-click, and right-click behavior with live video.
- [ ] Validate command-line file opening with real native libmpv playback.
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
