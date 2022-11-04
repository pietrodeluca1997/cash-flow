using CF.Core.Attributes;
using CF.Core.Repositories;
using CF.Report.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CF.Report.API.Data.QueryDatabase
{
    public class QueryDatabaseBaseRepository<TQueryDocument> : IQueryBaseRepository<TQueryDocument> where TQueryDocument : IQueryDocument
    {
        private readonly IMongoCollection<TQueryDocument> _collection;

        public QueryDatabaseBaseRepository(IOptions<QueryDatabaseSettings> noSQLDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(noSQLDatabaseSettings.Value.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(noSQLDatabaseSettings.Value.DatabaseName);
            _collection = database.GetCollection<TQueryDocument>(GetCollectionName(typeof(TQueryDocument)));
        }

        private string? GetCollectionName(Type documentType)
        {
            object[] attributes = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), inherit: true);
            BsonCollectionAttribute collectionAttribute = (BsonCollectionAttribute)attributes.First();
            return collectionAttribute.CollectionName;
        }

        public async Task CreateAsync(TQueryDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task CreateManyAsync(IList<TQueryDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            DeleteResult deleteResult = await _collection.DeleteOneAsync(filter: document => document.Id.Equals(id));

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IList<TQueryDocument>> FilterByAsync(Expression<Func<TQueryDocument, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).ToListAsync();
        }

        public async Task<TQueryDocument> GetByIdAsync(string id)
        {
            return await _collection.Find(document => document.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IList<TQueryDocument>> ListAllAsync()
        {
            return await _collection.Find(document => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(TQueryDocument document)
        {
            FilterDefinition<TQueryDocument> filter = Builders<TQueryDocument>.Filter.Eq(doc => doc.Id, document.Id);
            ReplaceOneResult result = await _collection.ReplaceOneAsync(filter, document);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
