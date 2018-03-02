using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoRepo.Entities;

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
            this.Collection.InsertOne(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await this.Collection.InsertOneAsync(entity).ConfigureAwait(false);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            this.Collection.InsertMany(entities);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await this.Collection.InsertManyAsync(entities).ConfigureAwait(false);
        }

        public TEntity GetById(TKey id)
        {
            var entity = this.Collection.FindSync(GetIdFilter(id)).FirstOrDefault();
            if (entity == null)
            {
                throw new ArgumentException($"{typeof(TEntity).Name} with id {id} not found");
            }

            return entity;
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            var entity = (await (await this.Collection.FindAsync(GetIdFilter(id)).ConfigureAwait(false)).ToListAsync().ConfigureAwait(false)).FirstOrDefault();
            if (entity == null)
            {
                throw new ArgumentException($"{typeof(TEntity).Name} with id {id} not found");
            }

            return entity;
        }

        public IList<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return this.Collection.FindSync(filter).ToList();
        }

        public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await (await this.Collection.FindAsync(filter).ConfigureAwait(false)).ToListAsync().ConfigureAwait(false);
        }

        public IList<TEntity> GetAll()
        {
            return this.Collection.FindSync(FilterDefinition<TEntity>.Empty).ToList();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await (await this.Collection.FindAsync(FilterDefinition<TEntity>.Empty).ConfigureAwait(false)).ToListAsync().ConfigureAwait(false);
        }

        public void Replace(TEntity entity)
        {
            this.Collection.ReplaceOne(GetIdFilter(entity.Id), entity);
        }

        public async Task ReplaceAsync(TEntity entity)
        {
            await this.Collection.ReplaceOneAsync(GetIdFilter(entity.Id), entity).ConfigureAwait(false);
        }

        public void Replace(IEnumerable<TEntity> entities)
        {
            var replacements = entities.Select(x => new ReplaceOneModel<TEntity>(GetIdFilter(x.Id), x));
            this.Collection.BulkWrite(replacements);
        }

        public async Task ReplaceAsync(IEnumerable<TEntity> entities)
        {
            var replacements = entities.Select(x => new ReplaceOneModel<TEntity>(GetIdFilter(x.Id), x));
            await this.Collection.BulkWriteAsync(replacements);
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
            this.Collection.DeleteMany(FilterDefinition<TEntity>.Empty);
        }

        public async Task DeleteAllAsync()
        {
            await this.Collection.DeleteManyAsync(FilterDefinition<TEntity>.Empty).ConfigureAwait(false);
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<TEntity, bool>> filter)
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

        private static FilterDefinition<TEntity> GetIdFilter(TKey id)
        {
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Eq(x => x.Id, id);
            return filter;
        }
    }
}