namespace CF.Core.Repositories
{
    public interface IMemoryDatabaseRepository
    {
        Task AddKeyAsync(string key, string value);
        Task AddKeyWithTimeSpanAsync(string key, string value, TimeSpan absoluteExpirationTime, TimeSpan slidingExpirationTime);
        Task<string> GetValueByKey(string key);
        Task DeleteByKey(string key);
    }
}
