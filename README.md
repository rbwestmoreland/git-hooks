# git-hooks

A command line tool to manage [git hooks](https://git-scm.com/docs/githooks).

## Table of Contents

* [Requirements](#requirements)
* [Install](#install)
  * [Linux / OSX](#linux--osx)
  * [Windows](#windows)
* [Directory Structure](#directory-structure)
  * [Repository](#repository)
  * [User](#user)
  * [Order of Execution](#order-of-execution)
* [Contributing](#contributing)
* [License](#license)

## Install

### Requirements

`git-hooks` requires `bash` and `git` to be installed and availabe in your `PATH`.

> Windows users may need to enable the [Windows Subsystem for Linux](https://docs.microsoft.com/en-us/windows/wsl/install-win10) for `bash` support.

### How can I check?

Open a shell or command prompt and run the following commands:

`git --version`

`bash -c "echo I have bash installed!"`

### Linux / OSX

```shell
curl -s https://raw.githubusercontent.com/rbwestmoreland/git-hooks/master/install/install.sh | sudo bash
```

### Windows

```powershell
(new-object Net.WebClient).DownloadString("https://raw.githubusercontent.com/rbwestmoreland/git-hooks/master/install/install.ps1") | iex
```

## Usage

### `git hooks install`

Run this command in any git repository to enable `git-hooks`. Once executed, you will have a new `.githooks` directory in the root of you git repository. See [directory structure](#directory-structure) for more information.

### `git hooks list`

This command lists all the known scripts in your repository and user `.githooks` directories. It will also list the scripts in the order they will be executed.

### `git hooks run <hook> [args]`

This command allows you to execute a hook manually (e.g. `git hooks run pre-commit`). This command is helpful when developing a new script.

### `git hooks uninstall`

You can run `git hooks uninstall` at anytime. This will revent to using the default `.git/hooks` directory. The generated `.githooks` directories in your repository and user directory will remain untouched. You can manually delete these, if you no longer wish to keep them.

### `git hooks help`

This command lists all the available commands and options.

## Directory Structure

After running `git hooks install`, you will have two directories available to place your scripts.

### Repository

Your repository's git hooks are located at the root of your git repository `.githooks`. 

> The `.githooks` directory should be committed to source control.

```
my-repository
├─ .git
└─ .githooks
    ├─ commit-msg
    │  └─ script_1.sh
    │  └─ script_2.sh
    └─ pre-commit
       └─ script_3.sh
       └─ script_4.sh
```

### User

Your personal git hooks are located in your home directory `~/.githooks`. 

```
~/.githooks
├─ commit-msg
│  └─ script_a.sh
│  └─ script_b.sh
└─ pre-commit
   └─ script_c.sh
   └─ script_d.sh
```

### Order of Execution

Using the [Repository](#repository) and [User](#user) example above, the following hooks would execute during `pre-commit`:

* `script_3.sh`
* `script_4.sh`
* `script_c.sh`
* `script_d.sh`

All scripts will execute, reguardless of the outcome of the previous script. The exit code returned to git is either the highest non-zero exit code from the scripts or zero.

> You can run `git hooks list` at any time to view the excution order of your scripts.

> You can run `git hooks run <hook> [args]` (e.g. `git hooks run pre-commit`) to execute a hook outside of the git hooks pipeline.

## Contributing

As I use this for my own projects, I know this might not be the perfect approach
for all the projects out there. If you have any ideas, just
[open an issue](https://github.com/rbwestmoreland/git-hooks/issues/new) and tell me what you think.

If you'd like to contribute, please fork the repository and make changes as
you'd like. Pull requests are welcomed.

## License

This project is licensed under the [MIT](LICENSE.md) license.