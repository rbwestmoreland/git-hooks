using System;
using System.Linq;

namespace GitHooks.Commands
{
    internal class Run : Command
    {
        public override bool IsMatch(Context context) => IsMatch(context, "run");

        public override int Execute(Context context)
        {
            if (!CheckPrerequisites())
                return 1;

            var hook = context.Args.ElementAtOrDefault(1);
            if (string.IsNullOrWhiteSpace(hook))
            {
                Output.WriteLine("fatal: missing required argument 'hook'");
                return 1;
            }

            var files = Paths.Hooks.GetAllFiles(hook).ToArray();
            if (!files.Any())
                return 0;

            Output.WriteLine("");

            var exit = 0;
            var args = GetScriptArguments(context);

            foreach (var file in files)
            {
                Output.WriteLine($"[{file}]");

                var lastExitCode = Bash.ExecuteScript(file, args, Output.WriteLine);
                exit = GetNewExitCode(exit, lastExitCode);

                Output.WriteLine("");
            }

            return exit;
        }

        private static string GetScriptArguments(Context context)
        {
            var hook = context.Args.ElementAt(1);
            var index = Environment.CommandLine.IndexOf(hook, StringComparison.Ordinal) + hook.Length + 1;
            var arguments = Environment.CommandLine.Length > index ? Environment.CommandLine.Substring(index) : string.Empty;
            return arguments;
        }

        private static int GetNewExitCode(int currExitCode, int lastExitCode)
        {
            if (lastExitCode == 0)
                return currExitCode;

            if (currExitCode == 0)
                return lastExitCode;

            var newExitCode = Math.Max(currExitCode, lastExitCode);
            return newExitCode;
        }
    }
}
