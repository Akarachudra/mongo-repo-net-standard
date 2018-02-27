using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepo.Entities
{
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }
    }
}