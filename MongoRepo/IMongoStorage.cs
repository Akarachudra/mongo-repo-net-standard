using MongoDB.Driver;
using MongoRepo.Entities;

namespace MongoRepo
{
    public interface IMongoStorage
    {
        IMongoCollection<TEntity> GetCollection<TEntity, TKey>()
            where TEntity : IEntity<TKey>;

        void DropCollection<TEntity, TKey>()
            where TEntity : IEntity<TKey>;
    }
}