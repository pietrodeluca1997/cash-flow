namespace CF.Account.API.Settings
{
    public class RelationalDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public RelationalDatabaseSettings()
        {

        }

        public RelationalDatabaseSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
