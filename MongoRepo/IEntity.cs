using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepo
{
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }
    }
}