using System;
using System.Linq;

namespace GitHooks.Commands
{
    internal abstract class Command
    {
        public abstract bool IsMatch(Context context);
        public abstract int Execute(Context context);

        protected bool IsMatch(Context context, params string[] values)
        {
            var argument = context.Args.ElementAtOrDefault(0);
            return values.Any(value => string.Equals(argument, value, StringComparison.OrdinalIgnoreCase));
        }

        protected static bool CheckPrerequisites()
        {
            if (!Bash.IsInstalled())
            {
                Output.WriteLine("fatal: bash installation not found");
                return false;
            }

            if (!Git.IsInstalled())
            {
                Output.WriteLine("fatal: git installation not found");
                return false;
            }

            if (!Git.IsRepository())
            {
                Output.WriteLine("fatal: not a git repository (or any of the parent directories)");
                return false;
            }

            return true;
        }
    }
}
