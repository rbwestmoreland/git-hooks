$script = (new-object Net.WebClient).DownloadString("https://raw.githubusercontent.com/rbwestmoreland/git-hooks/master/install/install.ps1")
Invoke-Expression $script
$env:Path = [System.Environment]::GetEnvironmentVariable("Path", "Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path", "User")
git hooks install
