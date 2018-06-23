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
            Output.WriteLine("");
            Output.WriteLine($"Hook: {context.Args.ElementAtOrDefault(1)}");
            Output.WriteLine($"Args: {context.Args.ElementAtOrDefault(2)}");
            Output.WriteLine("");
            return 0;
        }
    }
}
