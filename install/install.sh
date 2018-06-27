#!/usr/bin/env bash

# Detect OS
if [ "$(uname)" == "Darwin" ]; then
    os="osx"
elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    os="linux"
else
    os="$(uname)"
    echo "Unsupported Operating System: $os."
    exit 1
fi

# Variables
lib_path=/usr/local/lib/git-hooks
bin_path=/usr/local/bin/git-hooks
download_path=$lib_path/git-hooks.tar.gz

# Get the latest release
tag=$(curl -s https://api.github.com/repos/rbwestmoreland/git-hooks/releases/latest | grep -Eo "\"tag_name\":\s*\"(.*)\"" | cut -d'"' -f4)

# Download the tar
rm -rf $lib_path
mkdir $lib_path
curl -sL https://github.com/rbwestmoreland/git-hooks/releases/download/$tag/git-hooks-$tag-$os-x64.tar.gz > $download_path

# Uncompress to install directory
tar -xzf $download_path --directory=$lib_path
rm $download_path

# Make Executable
chmod +x $lib_path/git-hooks
ln -sfn $lib_path/git-hooks $bin_path
