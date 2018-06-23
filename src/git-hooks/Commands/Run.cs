using System;
using System.Diagnostics;
using System.Linq;

namespace GitHooks.Commands
{
    internal class Run : ICommand
    {
        public bool IsMatch(Context context)
        {
            var command = context.Args.ElementAtOrDefault(0);
            return string.Equals("run", command, StringComparison.OrdinalIgnoreCase);
        }

        public int Execute(Context context)
        {
            var hook = context.Args.ElementAtOrDefault(1);
            var args = context.Args.ElementAtOrDefault(2);
            var repositoryHooks = Paths.Hooks.GetRepositoryFiles(hook, Paths.Format.Absolute);
            var userProfileHooks = Paths.Hooks.GetRepositoryFiles(hook, Paths.Format.Absolute);

            Output.WriteLine("");
            Output.WriteLine($"Hook: {hook}");
            Output.WriteLine($"Args: {args}");
            Output.WriteLine($"Path: {Environment.CurrentDirectory}");

            //var process = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        UseShellExecute = false,
            //        RedirectStandardOutput = true,
            //        FileName = "git",
            //        CreateNoWindow = false,
            //        WorkingDirectory = Environment.CurrentDirectory
            //    }
            //};


            Output.WriteLine("");

            return 0;
        }
    }
}
