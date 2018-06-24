using System;
using System.Diagnostics;

namespace GitHooks
{
    public static class Git
    {
        private static readonly Process Process;

        static Git()
        {
            Process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    FileName = "git",
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    UseShellExecute = false
                }
            };
        }

        public static bool IsInstalled()
        {
            var isInstalled = false;

            try
            {
                Process.StartInfo.Arguments = "--version";
                Process.Start();

                if (!Process.WaitForExit(3000))
                    Process.Kill();

                isInstalled = Process.ExitCode == 0;
            }
            catch
            {
                // ignored
            }

            return isInstalled;
        }

        public static bool IsRepository()
        {
            var isRepository = false;

            try
            {
                Process.StartInfo.Arguments = "log -1";
                Process.Start();

                if (!Process.WaitForExit(3000))
                    Process.Kill();

                isRepository = Process.ExitCode == 0;
            }
            catch
            {
                // ignored
            }

            return isRepository;
        }

        public static string GetRootPath()
        {
            var path = default(string);

            try
            {
                Process.StartInfo.Arguments = "rev-parse --show-toplevel";
                Process.Start();

                if (!Process.WaitForExit(3000))
                    Process.Kill();

                path = Process.StandardOutput.ReadToEnd().Trim();
            }
            catch
            {
                // ignored
            }

            return path;
        }

        public static string GetConfig(string key)
        {
            var value = default(string);

            try
            {
                Process.StartInfo.Arguments = $"config {key}";
                Process.Start();

                if (!Process.WaitForExit(3000))
                    Process.Kill();

                value = Process.StandardOutput.ReadToEnd().Trim();
            }
            catch
            {
                // ignored
            }

            return value;
        }

        public static void SetConfig(string key, string value)
        {
            try
            {
                Process.StartInfo.Arguments = $"config {key} {value}";
                Process.Start();

                if (!Process.WaitForExit(3000))
                    Process.Kill();
            }
            catch
            {
                // ignored
            }
        }

        public static void UnsetConfig(string key)
        {
            try
            {
                Process.StartInfo.Arguments = $"config --unset {key}";
                Process.Start();

                if (!Process.WaitForExit(3000))
                    Process.Kill();
            }
            catch
            {
                // ignored
            }
        }

        public static string[] GetHooks()
        {
            return new[]
            {
                "applypatch-msg",
                "pre-applypatch",
                "post-applypatch",
                "pre-commit",
                "prepare-commit-msg",
                "commit-msg",
                "post-commit",
                "pre-rebase",
                "post-checkout",
                "post-merge",
                "pre-push",
                "pre-receive",
                "update",
                "post-receive",
                "post-update",
                "push-to-checkout",
                "pre-auto-gc",
                "post-rewrite",
                "rebase",
                "sendemail-validate",
                "fsmonitor-watchman"
            };
        }
    }
}
