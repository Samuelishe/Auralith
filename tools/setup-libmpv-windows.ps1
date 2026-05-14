[CmdletBinding()]
param(
    [ValidateSet("shinchiro", "zhongfly")]
    [string] $Source = "shinchiro",

    [string] $Destination,

    [string] $WorkDir,

    [switch] $KeepWorkDir
)

$ErrorActionPreference = "Stop"

function Get-RepoRoot {
    $scriptDir = Split-Path -Parent $PSCommandPath
    return (Resolve-Path (Join-Path $scriptDir "..")).Path
}

function Get-ReleaseApiUrl {
    param([string] $SelectedSource)

    if ($SelectedSource -eq "zhongfly") {
        return "https://api.github.com/repos/zhongfly/mpv-winbuild/releases/latest"
    }

    return "https://api.github.com/repos/shinchiro/mpv-winbuild-cmake/releases/latest"
}

function Find-Extractor {
    $sevenZip = Get-Command 7z -ErrorAction SilentlyContinue
    if ($sevenZip) {
        return @{ Kind = "7z"; Path = $sevenZip.Source }
    }

    $sevenZipAlt = Get-Command 7za -ErrorAction SilentlyContinue
    if ($sevenZipAlt) {
        return @{ Kind = "7z"; Path = $sevenZipAlt.Source }
    }

    $sevenZipZ = Get-Command 7zz -ErrorAction SilentlyContinue
    if ($sevenZipZ) {
        return @{ Kind = "7z"; Path = $sevenZipZ.Source }
    }

    $tar = Get-Command tar -ErrorAction SilentlyContinue
    if ($tar) {
        return @{ Kind = "tar"; Path = $tar.Source }
    }

    return $null
}

function Expand-ArchiveFile {
    param(
        [string] $ArchivePath,
        [string] $ExtractDir,
        [hashtable] $Extractor
    )

    New-Item -ItemType Directory -Force -Path $ExtractDir | Out-Null

    if ($ArchivePath.EndsWith(".zip", [StringComparison]::OrdinalIgnoreCase)) {
        Expand-Archive -Path $ArchivePath -DestinationPath $ExtractDir -Force
        return
    }

    if ($Extractor -and $Extractor.Kind -eq "7z") {
        & $Extractor.Path x "-o$ExtractDir" -y $ArchivePath | Out-Host
        return
    }

    if ($Extractor -and $Extractor.Kind -eq "tar") {
        & $Extractor.Path -xf $ArchivePath -C $ExtractDir
        if ($LASTEXITCODE -eq 0) {
            return
        }
    }

    throw "Could not extract '$ArchivePath'. Install 7-Zip, or manually extract a trusted mpv/libmpv Windows build and copy libmpv-2.dll plus companion DLLs into runtimes/win-x64/native."
}

$repoRoot = Get-RepoRoot
if (-not $Destination) {
    $Destination = Join-Path $repoRoot "runtimes/win-x64/native"
}

if (-not $WorkDir) {
    $WorkDir = Join-Path $repoRoot ".auralith/libmpv-windows"
}

$releaseApiUrl = Get-ReleaseApiUrl $Source
$downloadDir = Join-Path $WorkDir "downloads"
$extractDir = Join-Path $WorkDir "extract"

New-Item -ItemType Directory -Force -Path $downloadDir | Out-Null
New-Item -ItemType Directory -Force -Path $Destination | Out-Null
if (Test-Path $extractDir) {
    Remove-Item -Recurse -Force -Path $extractDir
}

Write-Host "Auralith Phase 1 libmpv setup"
Write-Host "Source: $Source"
Write-Host "Release API: $releaseApiUrl"
Write-Host "Destination: $Destination"

$headers = @{
    "User-Agent" = "Auralith-Phase1-libmpv-setup"
    "Accept" = "application/vnd.github+json"
}

$release = Invoke-RestMethod -Uri $releaseApiUrl -Headers $headers
$asset = $release.assets |
    Where-Object {
        $_.name -match "mpv-dev.*x86_64.*\.(7z|zip)$" -and
        $_.name -notmatch "i686|arm64|aarch64|symbols|debug"
    } |
    Select-Object -First 1

if (-not $asset) {
    throw "No suitable x86_64 libmpv development archive was found in the latest $Source release. Manually download a trusted Windows libmpv/mpv build that provides libmpv-2.dll."
}

$archivePath = Join-Path $downloadDir $asset.name
Write-Host "Selected asset: $($asset.name)"
Write-Host "Download URL: $($asset.browser_download_url)"

Invoke-WebRequest -Uri $asset.browser_download_url -OutFile $archivePath -Headers @{ "User-Agent" = "Auralith-Phase1-libmpv-setup" }

$extractor = Find-Extractor
if (-not $extractor -and -not $archivePath.EndsWith(".zip", [StringComparison]::OrdinalIgnoreCase)) {
    throw "No extractor was found. Install 7-Zip, or manually extract the archive and copy libmpv-2.dll plus companion DLLs into runtimes/win-x64/native."
}

Expand-ArchiveFile -ArchivePath $archivePath -ExtractDir $extractDir -Extractor $extractor

$libmpv = Get-ChildItem -Path $extractDir -Recurse -File -Filter "libmpv-2.dll" | Select-Object -First 1
if (-not $libmpv) {
    throw "Downloaded archive did not contain libmpv-2.dll. Try the other trusted source with -Source zhongfly, or inspect the archive manually."
}

$dlls = Get-ChildItem -Path $libmpv.Directory.FullName -File -Filter "*.dll"
foreach ($dll in $dlls) {
    Copy-Item -Path $dll.FullName -Destination (Join-Path $Destination $dll.Name) -Force
}

Write-Host ""
Write-Host "libmpv setup complete"
Write-Host "Release: $($release.name)"
Write-Host "Downloaded: $archivePath"
Write-Host "Extracted to: $extractDir"
Write-Host "libmpv-2.dll found: $($libmpv.FullName)"
Write-Host "DLLs copied: $($dlls.Count)"
Write-Host "Runtime path: $Destination"
Write-Host ""
Write-Host "Next:"
Write-Host "dotnet run --project src/Auralith.App/Auralith.App.csproj -- `"C:\path\to\video.mkv`""

if (-not $KeepWorkDir) {
    Write-Host ""
    Write-Host "Temporary download/extract files kept under ignored path: $WorkDir"
}
