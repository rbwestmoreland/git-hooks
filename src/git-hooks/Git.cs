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
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = "git",
                    CreateNoWindow = false,
                    WorkingDirectory = Environment.CurrentDirectory
                }
            };
        }

        public static bool IsRepository()
        {
            var isRepository = false;

            try
            {
                Process.StartInfo.Arguments = "log -1";
                Process.Start();
                Process.WaitForExit();
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
                path = Process.StandardOutput.ReadToEnd().Trim();
                Process.WaitForExit();
            }
            catch
            {
                // ignored
            }

            return path;
        }

        public static void SetConfig(string key, string value)
        {
            try
            {
                Process.StartInfo.Arguments = $"config {key} {value}";
                Process.Start();
                Process.WaitForExit();
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
                Process.WaitForExit();
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
