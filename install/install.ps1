[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

# Create a temporary folder for download
$tempFolder = Join-Path $env:TEMP "git-hooks"
New-Item $tempFolder -ItemType Directory -Force | Out-Null

# Get the latest release
$latestRelease = Invoke-WebRequest "https://api.github.com/repos/rbwestmoreland/git-hooks/releases/latest" | ConvertFrom-Json | Select-Object tag_name
$tag = $latestRelease.tag_name

# Download the zip
$client = New-Object "System.Net.WebClient"
$url = "https://github.com/rbwestmoreland/git-hooks/releases/download/$tag/git-hooks-$tag-win-x64.zip"
$zipFile = Join-Path $tempFolder "git-hooks.zip"
$client.DownloadFile($url, $zipFile)

# Unzip to Install Directory
$installationFolder = Join-Path $env:ProgramData "rbwestmoreland\git-hooks"
Remove-Item $installationFolder -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive $zipFile -DestinationPath $installationFolder -Force
Remove-Item $tempFolder -Recurse -Force

# Update PATH
$path = [System.Environment]::GetEnvironmentVariable("path", [System.EnvironmentVariableTarget]::User);
$paths = $path.Split(";") -inotlike "*rbwestmoreland\git-hooks*" 
$paths += $installationFolder
$path = $paths -join ";"
[System.Environment]::SetEnvironmentVariable("path", $path, [System.EnvironmentVariableTarget]::User)
$env:Path = [System.Environment]::GetEnvironmentVariable("Path", "Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path", "User")