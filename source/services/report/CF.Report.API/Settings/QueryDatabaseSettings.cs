namespace CF.Report.API.Settings
{
    public class QueryDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public QueryDatabaseSettings()
        {

        }

        public QueryDatabaseSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
