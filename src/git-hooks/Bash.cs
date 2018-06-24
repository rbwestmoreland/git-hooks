using System;
using System.Diagnostics;
using System.IO;

namespace GitHooks
{
    public static class Bash
    {
        private static readonly Process Process;

        static Bash()
        {
            Process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    FileName = "bash",
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
                Process.StartInfo.Arguments = "-c \"echo test\"";
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

        public static int ExecuteScript(string script, string arguments, Action<string> onOutput)
        {
            var exitCode = -1;
            var onDataReceived = new DataReceivedEventHandler((sender, args) =>
            {
                if (args.Data != null)
                    onOutput(args.Data);
            });

            try
            {
                Process.StartInfo.Arguments = $"-c \"sh {GetBashPath(script)} {arguments}\"";
                Process.Start();

                Process.OutputDataReceived += onDataReceived;
                Process.BeginOutputReadLine();

                Process.ErrorDataReceived += onDataReceived;
                Process.BeginErrorReadLine();

                Process.WaitForExit();

                Process.OutputDataReceived -= onDataReceived;
                Process.CancelOutputRead();

                Process.ErrorDataReceived -= onDataReceived;
                Process.CancelErrorRead();

                exitCode = Process.ExitCode;
            }
            catch
            {
                // ignored
            }

            return exitCode;
        }

        private static string GetBashPath(string script)
        {
            return Path.GetRelativePath(Environment.CurrentDirectory, script).Replace('\\', '/');
        }
    }
}
