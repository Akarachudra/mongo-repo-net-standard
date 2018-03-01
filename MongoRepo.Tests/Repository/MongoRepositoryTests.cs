using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
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
            var resultEntity = this.objectIdRepository.Get(x => x.Id != default(ObjectId).ToString()).SingleOrDefault();
            Assert.NotNull(resultEntity);
        }

        [Test]
        public void CanAutofillGuidIdOnInsert()
        {
            var testEntity = new GuidIdTestEntity();
            this.guidIdRepository.Insert(testEntity);
            var resultEntity = this.guidIdRepository.Get(x => x.Id != default(Guid)).SingleOrDefault();
            Assert.NotNull(resultEntity);
        }

        private static void InternalCanInsertAndGetWithFilter(Action<ObjectIdTestEntity> insert, Func<Expression<Func<ObjectIdTestEntity, bool>>, IList<ObjectIdTestEntity>> get)
        {
            var testEntity = new ObjectIdTestEntity
            {
                SomeData = 10
            };
            insert(testEntity);
            var result = get(x => x.SomeData == testEntity.SomeData);
            Assert.IsTrue(result.Count == 1);
        }
    }
}