using System.Linq;

namespace GitHooks.Commands
{
    internal class Unknown : Help
    {
        public override bool IsMatch(Context context) => true;

        public override int Execute(Context context)
        {
            var argument = context.Args.ElementAtOrDefault(0);
            Output.WriteLine($"fatal: unknown argument '{argument}'");
            Output.WriteLine("");
            base.Execute(context);
            return 1;
        }
    }
}
