namespace GitHooks.Commands
{
    internal class Help : Command
    {
        public override bool IsMatch(Context context) => IsMatch(context, "help", "--help", "?", "-?");

        public override int Execute(Context context)
        {
            Output.WriteLine("usage: git hooks <command> [args]");
            Output.WriteLine("");
            Output.WriteLine("   --version           Display the installed version");
            Output.WriteLine("   help                Display all available commands");
            Output.WriteLine("   install             Install git hooks in the current repository");
            Output.WriteLine("   uninstall           Uninstall git hooks from the current repository");
            Output.WriteLine("   list                List installed git hooks");
            Output.WriteLine("   run <hook> [args]   Run a specific git hook");
            Output.WriteLine("");
            return 0;
        }
    }
}
