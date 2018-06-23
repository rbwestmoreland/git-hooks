Function Get-ScriptDirectory {
    Split-Path $script:MyInvocation.MyCommand.Path
}

Function Get-ArtifactDirectory {
    $script_dir = Get-ScriptDirectory
    return "$script_dir\artifacts"
}

Function Get-ProjectPath {
    $script_dir = Get-ScriptDirectory
    return "$script_dir\..\src\git-hooks\git-hooks.csproj"
}

Function Get-ProjectVersion {
    $csproj = Get-ProjectPath
    return ([xml](Get-Content $csproj)).Project.PropertyGroup.Version
}
