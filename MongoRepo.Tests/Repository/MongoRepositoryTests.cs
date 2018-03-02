using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepo.Tests.Helpers;
using NUnit.Framework;

namespace MongoRepo.Tests.Repository
{
    [TestFixture]
    public class MongoRepositoryTests
    {
        private readonly IMongoRepository<ObjectIdTestEntity, string> objectIdRepository;
        private readonly IMongoRepository<GuidIdTestEntity, Guid> guidIdRepository;

        public MongoRepositoryTests()
        {
            var mongoStorage = StorageFabric.GetStorageBySettings();
            this.objectIdRepository = new MongoRepository<ObjectIdTestEntity, string>(mongoStorage);
            this.guidIdRepository = new MongoRepository<GuidIdTestEntity, Guid>(mongoStorage);
        }

        [SetUp]
        public void RunBeforeTest()
        {
            this.objectIdRepository.DeleteAll();
            this.guidIdRepository.DeleteAll();
        }

        [Test]
        public void CanInsertAndGetWithFilter()
        {
            InternalCanInsertAndGetWithFilter(this.objectIdRepository.Insert, this.objectIdRepository.Get);
        }

        [Test]
        public void CanInsertAndGetWithFilterAsync()
        {
            InternalCanInsertAndGetWithFilter(
                entity => this.objectIdRepository.InsertAsync(entity).Wait(),
                filter => this.objectIdRepository.GetAsync(filter).Result);
        }

        [Test]
        public void CanAutofillObjectIdOnInsert()
        {
            var testEntity = new ObjectIdTestEntity();
            this.objectIdRepository.Insert(testEntity);
            var resultEntity = this.objectIdRepository.Get(x => x.Id != default(ObjectId).ToString()).FirstOrDefault();
            Assert.NotNull(resultEntity);
        }

        [Test]
        public void CanAutofillGuidIdOnInsert()
        {
            var testEntity = new GuidIdTestEntity();
            this.guidIdRepository.Insert(testEntity);
            var resultEntity = this.guidIdRepository.Get(x => x.Id != default(Guid)).FirstOrDefault();
            Assert.NotNull(resultEntity);
        }

        [Test]
        public void CanInsertWithClientSideGuidId()
        {
            var testEntity = new GuidIdTestEntity
            {
                Id = Guid.NewGuid()
            };
            this.guidIdRepository.Insert(testEntity);
            var resultEntity = this.guidIdRepository.Get(x => x.Id == testEntity.Id).FirstOrDefault();
            Assert.NotNull(resultEntity);
        }

        [Test]
        public void CanInsertSeveralDocumentsAndGetAll()
        {
            InternalInsertSeveralDocumentsAndGetAll(this.objectIdRepository.Insert, this.objectIdRepository.GetAll);
        }

        [Test]
        public void CanInsertSeveralDocumentsAndGetAllAsync()
        {
            InternalInsertSeveralDocumentsAndGetAll(
                entities => this.objectIdRepository.InsertAsync(entities).Wait(),
                () => this.objectIdRepository.GetAllAsync().Result);
        }

        [Test]
        public void CanGetByIdAndThrowExceptionIfNotFound()
        {
            InternalCanGetByIdAndThrowExceptionIfNotFound(this.guidIdRepository.Insert, this.guidIdRepository.GetById);
        }

        [Test]
        public void CanGetByIdAndThrowExceptionIfNotFoundAsync()
        {
            InternalCanGetByIdAndThrowExceptionIfNotFound(
                entity => this.guidIdRepository.InsertAsync(entity).Wait(),
                key => this.guidIdRepository.GetByIdAsync(key).Result);
        }

        [Test]
        public void CanReplaceEntity()
        {
            InternalCanReplaceEntity(
                this.guidIdRepository.Insert,
                this.guidIdRepository.GetById,
                this.guidIdRepository.Replace);
        }

        [Test]
        public void CanReplaceEntityAsync()
        {
            InternalCanReplaceEntity(
                entity => this.guidIdRepository.InsertAsync(entity).Wait(),
                key => this.guidIdRepository.GetByIdAsync(key).Result,
                entity => this.guidIdRepository.ReplaceAsync(entity).Wait());
        }

        [Test]
        public void CanReplaceSeveralEntities()
        {
            InternalCanReplaceSeveralEntities(
                this.guidIdRepository.Insert,
                this.guidIdRepository.GetAll,
                this.guidIdRepository.Replace);
        }

        [Test]
        public void CanReplaceSeveralEntitiesAsync()
        {
            InternalCanReplaceSeveralEntities(
                entities => this.guidIdRepository.InsertAsync(entities).Wait(),
                () => this.guidIdRepository.GetAllAsync().Result,
                entities => this.guidIdRepository.ReplaceAsync(entities).Wait());
        }

        [Test]
        public void CanUpdateEntity()
        {
            this.InternalCanUpdateEntity(this.guidIdRepository.Insert, this.guidIdRepository.GetById, this.guidIdRepository.Update);
        }

        [Test]
        public void CanUpdateEntityAsync()
        {
            this.InternalCanUpdateEntity(
                entity => this.guidIdRepository.InsertAsync(entity).Wait(),
                guid => this.guidIdRepository.GetByIdAsync(guid).Result,
                (f, u) => this.guidIdRepository.UpdateAsync(f, u).Wait());
        }

        private static void InternalCanInsertAndGetWithFilter(
            Action<ObjectIdTestEntity> insert,
            Func<Expression<Func<ObjectIdTestEntity, bool>>, IList<ObjectIdTestEntity>> get)
        {
            var testEntity = new ObjectIdTestEntity
            {
                SomeData = 10
            };
            insert(testEntity);
            var result = get(x => x.SomeData == testEntity.SomeData);
            Assert.IsTrue(result.Count == 1);
        }

        private static void InternalInsertSeveralDocumentsAndGetAll(Action<IEnumerable<ObjectIdTestEntity>> insert, Func<IList<ObjectIdTestEntity>> getAll)
        {
            var testEntities = new[]
            {
                new ObjectIdTestEntity
                {
                    SomeData = 5
                },
                new ObjectIdTestEntity
                {
                    SomeData = 10
                }
            };
            insert(testEntities);
            var resultEntities = getAll();
            CollectionAssert.AreEquivalent(testEntities.Select(x => new { x.SomeData }), resultEntities.Select(x => new { x.SomeData }));
        }

        private static void InternalCanGetByIdAndThrowExceptionIfNotFound(Action<GuidIdTestEntity> insert, Func<Guid, GuidIdTestEntity> getById)
        {
            var testEntity = new GuidIdTestEntity
            {
                Id = Guid.NewGuid(),
                SomeData = 10
            };
            insert(testEntity);
            var resultEntity = getById(testEntity.Id);
            Assert.AreEqual(resultEntity.SomeData, testEntity.SomeData);

            // Test exception throwing
            var notExistsKey = Guid.NewGuid();
            try
            {
                getById(notExistsKey);
            }
            catch (Exception e)
            {
                var argumentException = e as ArgumentException;
                if (e is AggregateException aggregateException)
                {
                    argumentException = aggregateException.InnerExceptions.First() as ArgumentException;
                }

                Assert.NotNull(argumentException);
                Assert.AreEqual($"{typeof(GuidIdTestEntity).Name} with id {notExistsKey} not found", argumentException.Message);
            }
        }

        private static void InternalCanReplaceEntity(Action<GuidIdTestEntity> insert, Func<Guid, GuidIdTestEntity> getById, Action<GuidIdTestEntity> replace)
        {
            var testEntity = new GuidIdTestEntity
            {
                Id = Guid.NewGuid(),
                SomeData = 10,
                AnotherData = 5
            };
            insert(testEntity);
            var replaceWith = new GuidIdTestEntity
            {
                Id = testEntity.Id,
                SomeData = 5,
                AnotherData = 10
            };
            replace(replaceWith);
            var resultEntity = getById(testEntity.Id);
            Assert.IsTrue(ObjectsComparer.AreEqual(replaceWith, resultEntity));
        }

        private static void InternalCanReplaceSeveralEntities(
            Action<IEnumerable<GuidIdTestEntity>> insert,
            Func<IEnumerable<GuidIdTestEntity>> getAll,
            Action<IEnumerable<GuidIdTestEntity>> replace)
        {
            var testEntities = new[]
            {
                new GuidIdTestEntity
                {
                    Id = Guid.NewGuid(),
                    SomeData = 10,
                    AnotherData = 5
                },
                new GuidIdTestEntity
                {
                    Id = Guid.NewGuid(),
                    SomeData = 1,
                    AnotherData = 3
                }
            };
            insert(testEntities);
            var replaceWithEntities = new[]
            {
                new GuidIdTestEntity
                {
                    Id = testEntities[0].Id,
                    SomeData = 3,
                    AnotherData = 8
                },
                new GuidIdTestEntity
                {
                    Id = testEntities[1].Id,
                    SomeData = 5,
                    AnotherData = 6
                }
            };
            replace(replaceWithEntities);
            var resultEntities = getAll();
            CollectionAssert.AreEquivalent(
                replaceWithEntities.Select(x => new { x.Id, x.SomeData, x.AnotherData }),
                resultEntities.Select(x => new { x.Id, x.SomeData, x.AnotherData }));
        }

        private void InternalCanUpdateEntity(
            Action<GuidIdTestEntity> insert,
            Func<Guid, GuidIdTestEntity> getById,
            Action<Expression<Func<GuidIdTestEntity, bool>>, UpdateDefinition<GuidIdTestEntity>> update)
        {
            var testEntity = new GuidIdTestEntity
            {
                Id = Guid.NewGuid(),
                SomeData = 5,
                AnotherData = 10
            };
            insert(testEntity);
            var updater = this.guidIdRepository.Updater;
            var updateDefinition = updater.Set(x => x.SomeData, 10);
            update(x => x.Id == testEntity.Id, updateDefinition);
            var resultEntity = getById(testEntity.Id);
            Assert.AreEqual(10, resultEntity.SomeData);
            Assert.AreEqual(testEntity.AnotherData, resultEntity.AnotherData);
        }
    }
}