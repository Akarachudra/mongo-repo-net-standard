using MongoDB.Driver;
using MongoRepo.Entities;

namespace MongoRepo
{
    public class MongoStorage : IMongoStorage
    {
        public MongoStorage(MongoClientSettings settings, string dataBaseName)
        {
            
        }

        public MongoStorage(string connectionString, string dataBaseName)
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