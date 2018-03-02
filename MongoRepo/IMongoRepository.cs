using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoRepo.Entities;

namespace MongoRepo
{
    public interface IMongoRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        IMongoCollection<TEntity> Collection { get; }

        UpdateDefinitionBuilder<TEntity> Updater { get; }

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        Task InsertAsync(IEnumerable<TEntity> entities);

        TEntity GetById(TKey id);

        Task<TEntity> GetByIdAsync(TKey id);

        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);

        IList<TEntity> GetAll();

        Task<IList<TEntity>> GetAllAsync();

        void Replace(TEntity entity);

        Task ReplaceAsync(TEntity entity);

        void Replace(IEnumerable<TEntity> entities);

        Task ReplaceAsync(IEnumerable<TEntity> entities);

        void Update(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateDefinition);

        Task UpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateDefinition);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(TEntity[] entities);

        Task DeleteAsync(TEntity[] entities);

        void Delete(TKey id);

        Task DeleteAsync(TKey id);

        void Delete(TKey[] ids);

        Task DeleteAsync(TKey[] ids);

        void DeleteAll();

        Task DeleteAllAsync();

        long Count();

        Task<long> CountAsync();

        long Count(Expression<Func<TEntity, bool>> filter);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter);

        bool Exists(TKey id);

        Task<bool> ExistsAsync(TKey id);
    }
}