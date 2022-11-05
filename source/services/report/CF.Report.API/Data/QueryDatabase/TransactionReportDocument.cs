using CF.Core.Attributes;
using CF.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CF.Report.API.Data.QueryDatabase
{
    [BsonCollection("Transactions")]
    public class TransactionReportDocument : IQueryDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
