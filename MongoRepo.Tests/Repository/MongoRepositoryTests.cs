using NUnit.Framework;

namespace MongoRepo.Tests.Repository
{
    [TestFixture]
    public class MongoRepositoryTests
    {
        private readonly IMongoRepository<TestEntity, string> mongoRepository;

        public MongoRepositoryTests()
        {
            var mongoStorage = StorageFabric.GetStorageBySettings();
            this.mongoRepository = new MongoRepository<TestEntity, string>(mongoStorage);
        }

        [SetUp]
        public void RunBeforeTest()
        {
            this.mongoRepository.DeleteAll();
        }

        [Test]
        public void CanInsertAndGetEntity()
        {
            var testEntity = new TestEntity
            {
                SomeData = 10
            };
            this.mongoRepository.Insert(testEntity);
            var result = this.mongoRepository.Get(x => x.SomeData == testEntity.SomeData);
            Assert.IsTrue(result.Count == 1);
        }
    }
}