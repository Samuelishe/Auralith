# Windows libmpv Runtime

This directory is the Phase 1 development-time location for Windows native libmpv files.

Expected files after setup:

- `libmpv-2.dll`
- Required companion DLLs from the same compatible Windows libmpv/mpv build

Native binaries are intentionally not committed to git. Use the dev helper script from the repository root:

```powershell
.\tools\setup-libmpv-windows.ps1
```

This is not final release packaging. Future Windows releases should bundle the native libmpv runtime so ordinary users do not manually download or place DLLs.
