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
