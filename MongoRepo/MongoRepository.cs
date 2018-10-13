using MongoDB.Bson;
using MongoRepo.Entities;

namespace MongoRepo
{
    public class MongoRepository<TEntity> : GenericMongoRepository<TEntity, ObjectId>
        where TEntity : IEntity<ObjectId>
    {
        public MongoRepository(IMongoStorage mongoStorage)
            : base(mongoStorage)
        {
        }
    }
}