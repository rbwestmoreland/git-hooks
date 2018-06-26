namespace GitHooks.Commands
{
    internal class List : Command
    {
        public override bool IsMatch(Context context) => IsMatch(context, "list");

        public override int Execute(Context context)
        {
            if (!CheckPrerequisites())
                return 1;

            Output.WriteLine("");

            foreach (var hook in Git.GetHooks())
            {
                foreach (var file in Paths.Hooks.GetAllFiles(hook))
                {
                    Output.WriteLine($"[{hook}] {file}");
                }
            }

            Output.WriteLine("");

            return 0;
        }


    }
}
