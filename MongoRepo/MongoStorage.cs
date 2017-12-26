using MongoDB.Driver;

namespace MongoRepo
{
    public class MongoStorage : IMongoStorage
    {
        public MongoStorage(MongoClientSettings settings)
        {
            
        }

        public MongoStorage(string connectionString)
        {
            
        }

        public IMongoCollection<TEntity> GetCollection<TEntity, TKey>()
            where TEntity : IEntity<TKey>
        {
            throw new System.NotImplementedException();
        }

        public void DropCollection<TEntity, TKey>()
            where TEntity : IEntity<TKey>
        {
            throw new System.NotImplementedException();
        }
    }
}