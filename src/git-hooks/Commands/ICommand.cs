namespace GitHooks.Commands
{
    internal interface ICommand
    {
        bool IsMatch(Context context);
        int Execute(Context context);
    }
}
