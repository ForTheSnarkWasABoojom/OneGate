namespace OneGate.Backend.Core.Base.Database
{
    public static class ConnectionString
    {
        public static string Build(DatabaseConnectionOptions options)
        {
            return $"Host={options.Host};" +
                   $"Port={options.Port};" +
                   $"Database={options.Database};" +
                   $"Username={options.Username};" +
                   $"Password={options.Password}";
        }
    }
}