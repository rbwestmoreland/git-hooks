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

Function Get-TestProjectPath {
    $script_dir = Get-ScriptDirectory
    return "$script_dir\..\src\git-hooks.tests\git-hooks.tests.csproj"
}

Function Get-ProjectVersion {
    $csproj = Get-ProjectPath
    return ([xml](Get-Content $csproj)).Project.PropertyGroup.Version
}

Function Invoke-Bash ($Command) {
    return $(C:\Windows\System32\bash.exe -c "$Command") 
}

Function Get-WslPath($Path) {
    return Invoke-Bash("wslpath -a '$Path'")
}