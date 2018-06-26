namespace GitHooks.Commands
{
    internal class Version : Command
    {
        public override bool IsMatch(Context context) => IsMatch(context, "--version");

        public override int Execute(Context context)
        {
            Output.WriteLine($"git-hooks v{context.Version}");
            return 0;
        }
    }
}
