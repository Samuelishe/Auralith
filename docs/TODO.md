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
- [ ] Define native libmpv loading strategy.
- [ ] Define Windows packaging expectations.
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
- [x] Confirm controlled startup when native libmpv is missing.
- [ ] Provide compatible Windows native `libmpv.2` beside app output for local spike validation.
- [ ] Validate embedded video rendering.
- [ ] Validate overlay z-order above video.
- [ ] Validate play/pause, seek, volume, duration/position updates.
- [ ] Validate fullscreen and resize behavior.
- [ ] Validate single-click, double-click, and right-click behavior with live video.

## Future

- [ ] Define exact .NET project layout after Phase 1 is approved.
- [ ] Choose dependency versioning strategy.
- [ ] Define coding style and formatting rules.
- [ ] Define build/test verification commands.
- [ ] Investigate libmpv packaging options per OS.
- [ ] Define first UI design-token draft.
- [ ] Draft platform-specific feature capability matrix.
