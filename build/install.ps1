# init
if (!$PSScriptRoot) { $PSScriptRoot = Split-Path $script:MyInvocation.MyCommand.Path }

# imports
. $PSScriptRoot\common.ps1

# vars
$version = Get-ProjectVersion
$zipFile = "$PSScriptRoot\artifacts\git-hooks-v$version-win-x64.zip"

Write-Output "--------------------------------------"
Write-Output "Installing version v$version"
Write-Output "--------------------------------------"

# uninstall and install
$installationFolder = Join-Path $env:ProgramData "rbwestmoreland\git-hooks"
Remove-Item $installationFolder -Recurse -Force -ErrorAction SilentlyContinue
Microsoft.PowerShell.Archive\Expand-Archive $zipFile -DestinationPath $installationFolder -Force

# update PATH
$path = [System.Environment]::GetEnvironmentVariable("path", [System.EnvironmentVariableTarget]::User);
$paths = $path.Split(";") -inotlike "*rbwestmoreland\git-hooks*" 
$paths += $installationFolder
$path = $paths -join ";"
[System.Environment]::SetEnvironmentVariable("path", $path, [System.EnvironmentVariableTarget]::User)

# success
Write-Host "Success"
Write-Host ""