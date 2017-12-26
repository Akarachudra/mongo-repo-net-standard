using MongoDB.Driver;

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