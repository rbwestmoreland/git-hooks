# init
if (!$PSScriptRoot) { $PSScriptRoot = Split-Path $script:MyInvocation.MyCommand.Path }

# imports
. $PSScriptRoot\build.ps1

if ($LASTEXITCODE -eq 0) {
    . $PSScriptRoot\install.ps1
}