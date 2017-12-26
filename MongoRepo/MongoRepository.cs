using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoRepo
{
    public class MongoRepository<TEntity, TKey> : IMongoRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        public MongoRepository(IMongoStorage mongoStorage)
        {
            this.Collection = mongoStorage.GetCollection<TEntity, TKey>();
        }

        public IMongoCollection<TEntity> Collection { get; }

        public UpdateDefinitionBuilder<TEntity> Updater => Builders<TEntity>.Update;

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public TEntity[] Get(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity[]> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Replace(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Replace(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceAsync(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateDefinition)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateDefinition)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public bool Exists(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}