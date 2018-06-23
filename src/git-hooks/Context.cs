namespace GitHooks
{
    internal class Context
    {
        public string Version { get; }
        public string[] Args { get; }

        public Context(string[] args)
        {
            Version = GetSemVer();
            Args = args ?? new string[0];
        }

        private static string GetSemVer()
        {
            var version = typeof(Context).Assembly.GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
