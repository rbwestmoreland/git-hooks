using System;
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
            var exit = 0;
            var hook = context.Args.ElementAtOrDefault(1);
            var args = context.Args.ElementAtOrDefault(2);
            var files = Paths.Hooks.GetAllFiles(hook).ToArray();

            if (!files.Any())
                return exit;

            Output.WriteLine("");

            foreach (var file in files)
            {
                Output.WriteLine($"[{file}]");

                var lastExitCode = Bash.ExecuteScript(file, args, Output.WriteLine);
                exit = GetNewExitCode(exit, lastExitCode);

                Output.WriteLine("");
            }

            return exit;
        }

        private static int GetNewExitCode(int currExitCode, int lastExitCode)
        {
            if (lastExitCode == 0)
                return currExitCode;

            return currExitCode == 0 ? lastExitCode : Math.Max(currExitCode, lastExitCode);
        }
    }
}
