# init
if (!$PSScriptRoot) { $PSScriptRoot = Split-Path $script:MyInvocation.MyCommand.Path }

# imports
. $PSScriptRoot\common.ps1

# vars
$version = Get-ProjectVersion
$zipFile = ".\artifacts\v$version\git-hooks-v$version-win-x64.zip"

# uninstall and install
$installationFolder = Join-Path $env:ProgramData "rbwestmoreland\git-hooks\v$version"
Remove-Item $installationFolder -Recurse -Force -ErrorAction SilentlyContinue
Microsoft.PowerShell.Archive\Expand-Archive $zipFile -DestinationPath $installationFolder -Force

# update PATH
$path = [System.Environment]::GetEnvironmentVariable("path", [System.EnvironmentVariableTarget]::User);
$paths = $path.Split(";") -inotlike "*rbwestmoreland\git-hooks*" 
$paths += $installationFolder
$path = $paths -join ";"
[System.Environment]::SetEnvironmentVariable("path", $path, [System.EnvironmentVariableTarget]::User)

# success
Write-Host "Successfully installed version ($version)"