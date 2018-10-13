using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepo.Entities
{
    public abstract class BaseEntity : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}