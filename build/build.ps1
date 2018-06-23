# init
if (!$PSScriptRoot) { $PSScriptRoot = Split-Path $script:MyInvocation.MyCommand.Path }

# imports
. $PSScriptRoot\common.ps1

# vars
$artifact_dir = Get-ArtifactDirectory
$csproj = Get-ProjectPath
$version = Get-ProjectVersion
$distros = @("win-x64")

# script
Write-Output "----------------------------"
Write-Output "Build Parameters"
Write-Output "----------------------------"
Write-Output "Artifact Dir: $artifact_dir"
Write-Output "Project Path: $csproj"
Write-Output "Project Version: $version"
Write-Output "Distributions: $distros"
Write-Output "----------------------------"
Write-Output ""

Write-Output "----------------------------"
Write-Output "Removing Previous Artifacts"
Write-Output "----------------------------"
Remove-Item -Recurse -Force $artifact_dir -ErrorAction silentlycontinue
Write-Output "Success"

foreach ($distro in $distros) {
    Write-Output ""
    Write-Output "----------------------------"
    Write-Output "Building $distro Artifact"
    Write-Output "----------------------------"
    $output_dir = "$artifact_dir/v$version/$distro"
    $zip_path = "$artifact_dir/v$version/git-hooks-v$version-$distro.zip"
    dotnet publish $csproj -c "Release" -r "$distro" -o $output_dir --self-contained
    Compress-Archive -Path $output_dir/* -DestinationPath $zip_path
    Remove-Item -Recurse -Force $output_dir
    Write-Output ""
    Write-Output "Success"
}
