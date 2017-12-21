using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepo
{
    public class Entity<TKey> : IEntity<TKey>
        where TKey : class
    {
        [BsonId]
        public TKey Id { get; set; }
    }
}