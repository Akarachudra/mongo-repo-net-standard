using System;
using MongoDB.Driver;
using NUnit.Framework;

namespace MongoRepo.Tests.Storage
{
    [TestFixture]
    public class MongoStorageTests
    {
        [Test]
        public void CanGetCollection()
        {
            var mongoStorage = new MongoStorage(
                new MongoClientSettings
                {
                    Server = new MongoServerAddress("localhost", 27017)
                },
                "MongoRepoTestBase");
            var collection = mongoStorage.GetCollection<WithCollectionAttributeEntity, Guid>();
            Assert.NotNull(collection);
        }

        [Test]
        public void CanThrowExceptionIfThereNoCollectionNameAttribute()
        {
        }
    }
}