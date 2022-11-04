namespace CF.Account.API.Settings
{
    public class MemoryDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public int Port { get; set; }

        public MemoryDatabaseSettings()
        {

        }

        public MemoryDatabaseSettings(string connectionString, int port)
        {
            ConnectionString = connectionString;
            Port = port;
        }
    }
}
