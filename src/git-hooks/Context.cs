namespace GitHooks
{
    public class Context
    {
        public string Version { get; }
        public string Command { get; }

        public Context(string[] args)
        {
            Version = GetSemVer();
        }

        private static string GetSemVer()
        {
            var version = typeof(Context).Assembly.GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
