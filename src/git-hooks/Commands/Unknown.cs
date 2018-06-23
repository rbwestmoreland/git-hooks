using System.Linq;

namespace GitHooks.Commands
{
    internal class Unknown : ICommand
    {
        public bool IsMatch(Context context) => true;

        public int Execute(Context context)
        {
            var command = context.Args.ElementAtOrDefault(0);
            Output.WriteLine($"error: unknown command '{command}'");
            new Help().Execute(context);
            return 1;
        }
    }
}
