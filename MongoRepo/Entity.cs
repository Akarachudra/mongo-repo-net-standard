using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepo
{
    public class Entity<TKey> : IEntity<TKey>
    {
        [BsonId]
        public TKey Id { get; set; }
    }
}