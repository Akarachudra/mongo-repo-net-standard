using System;
using System.Reflection;
using MongoDB.Driver;
using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo
{
    public class MongoStorage : IMongoStorage
    {
        private readonly IMongoDatabase database;

        public MongoStorage(MongoClientSettings settings, string dataBaseName)
        {
            var client = new MongoClient(settings);
            this.database = client.GetDatabase(dataBaseName);
        }

        public MongoStorage(string connectionString, string dataBaseName)
        {
            var client = new MongoClient(connectionString);
            this.database = client.GetDatabase(dataBaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity, TKey>()
            where TEntity : IEntity<TKey>
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            foreach (var attr in typeInfo.GetCustomAttributes(typeof(CollectionNameAttribute)))
            {
                if (attr is CollectionNameAttribute attribute)
                {
                    var collectionName = attribute.Name;
                    if (string.IsNullOrEmpty(collectionName))
                    {
                        throw new ArgumentException(
                            $"There is empty collection name at {typeof(CollectionNameAttribute).Name} in {typeInfo.Name}");
                    }

                    return this.database.GetCollection<TEntity>(collectionName);
                }
            }

            throw new ArgumentException(
                $"There is no {typeof(CollectionNameAttribute).Name} attribute at {typeInfo.Name}");
        }

        public void DropCollection<TEntity, TKey>()
            where TEntity : IEntity<TKey>
        {
            throw new NotImplementedException();
        }
    }
}