namespace CF.Report.API.Settings
{
    public class QueryDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public QueryDatabaseSettings()
        {

        }

        public QueryDatabaseSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
    }
}
