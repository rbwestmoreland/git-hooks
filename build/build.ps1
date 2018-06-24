# init
if (!$PSScriptRoot) { $PSScriptRoot = Split-Path $script:MyInvocation.MyCommand.Path }

# imports
. $PSScriptRoot\common.ps1

# vars
$csproj = Get-ProjectPath
$version = Get-ProjectVersion
$artifacts = Get-ArtifactDirectory
$distros = @("win-x64", "linux-x64", "osx-x64")

# script
Write-Output "--------------------------------------"
Write-Output "Build Parameters"
Write-Output "--------------------------------------"
Write-Output "Project Path: $csproj"
Write-Output "Project Version: $version"
Write-Output "Artifacts Path: $artifacts"
Write-Output "Distributions: $distros"
Write-Output "--------------------------------------"
Write-Output ""

Write-Output "--------------------------------------"
Write-Output "Removing Previous Artifacts"
Write-Output "--------------------------------------"
Remove-Item -Recurse -Force $artifacts -ErrorAction silentlycontinue
Write-Output "Success"

foreach ($distro in $distros) {
    Write-Output ""
    Write-Output "--------------------------------------"
    Write-Output "Building $distro Artifacts"
    Write-Output "--------------------------------------"
    $output = "$artifacts/$distro"
    $compress_path = "$artifacts/git-hooks-v$version-$distro"
    dotnet publish $csproj -c "Release" -r "$distro" -o $output --self-contained

    Write-Output "--------------------------------------"
    Write-Output "Compressing $distro Artifacts (zip)"
    Write-Output "--------------------------------------"
    $zip_path = "$compress_path.zip"
    Compress-Archive -Path $output/* -DestinationPath $zip_path
    Write-Output "Success"

    Write-Output "--------------------------------------"
    Write-Output "Compressing $distro Artifacts (tar.gz)"
    Write-Output "--------------------------------------"
    $wsl_output = Get-WslPath($output)
    $wsl_tar_path = Get-WslPath("$compress_path.tar.gz")
    bash -c "tar -zcf $wsl_tar_path --directory=$wsl_output ."
    Write-Output "Success"

    Write-Output "--------------------------------------"
    Write-Output "Removing Build Artifacts"
    Write-Output "--------------------------------------"
    Remove-Item -Recurse -Force $output
    Write-Output "Success"
}
