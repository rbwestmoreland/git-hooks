using System;
using System.IO;
using System.Text;

namespace GitHooks.Commands
{
    internal class Install : Command
    {
        public override bool IsMatch(Context context) => IsMatch(context, "install");

        public override int Execute(Context context)
        {
            if (!CheckPrerequisites())
                return 1;

            CreateGitDirectory();
            CreateRepositoryDirectory();
            CreateUserDirectory();

            Git.SetConfig("core.hooksPath", ".git/.githooks");

            return 0;
        }

        private static void CreateGitDirectory()
        {
            var directory = Paths.Install.GetRepositoryPath();
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            foreach (var hook in Git.GetHooks())
            {
                var file = Path.Combine(directory, hook);
                if (!File.Exists(file))
                    File.WriteAllText(file, $"#!/usr/bin/env bash{Environment.NewLine}git hooks run {hook} \"$@\"");
            }
        }

        private static void CreateRepositoryDirectory()
        {
            var path = Paths.Hooks.GetRepositoryPath();
            CreateDirectory(path);
            CreateInstallScripts(path);
        }

        private static void CreateUserDirectory()
        {
            var path = Paths.Hooks.GetUserProfilePath();
            CreateDirectory(path);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var gitattributes = Path.Combine(path, ".gitattributes");
            if (!File.Exists(gitattributes))
                File.WriteAllText(gitattributes, "* text eol=lf", new UTF8Encoding(false));

            foreach (var hook in Git.GetHooks())
            {
                var subfolder = Path.Combine(path, hook);
                if (!Directory.Exists(subfolder))
                    Directory.CreateDirectory(subfolder);
            }
        }

        private static void CreateInstallScripts(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var shellScriptPath = Path.Combine(path, "enable.sh");
            var shellScriptBuilder = new StringBuilder();
            shellScriptBuilder.Append("#!/usr/bin/env bash\n");
            shellScriptBuilder.Append("curl -s https://raw.githubusercontent.com/rbwestmoreland/git-hooks/master/install/install.sh | bash\n");
            shellScriptBuilder.Append("git hooks install");
            File.WriteAllText(shellScriptPath, shellScriptBuilder.ToString(), new UTF8Encoding(false));

            var powershellScriptPath = Path.Combine(path, "enable.ps1");
            var powershellScriptBuilder = new StringBuilder();
            powershellScriptBuilder.AppendLine("$script = (new-object Net.WebClient).DownloadString(\"https://raw.githubusercontent.com/rbwestmoreland/git-hooks/master/install/install.ps1\")");
            powershellScriptBuilder.AppendLine("Invoke-Expression $script");
            powershellScriptBuilder.AppendLine("$env:Path = [System.Environment]::GetEnvironmentVariable(\"Path\", \"Machine\") + \";\" + [System.Environment]::GetEnvironmentVariable(\"Path\", \"User\")");
            powershellScriptBuilder.AppendLine("git hooks install");
            File.WriteAllText(powershellScriptPath, powershellScriptBuilder.ToString(), new UTF8Encoding(false));
        }
    }
}
