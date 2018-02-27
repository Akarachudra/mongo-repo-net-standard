using System;
using NUnit.Framework;

namespace MongoRepo.Tests.Storage
{
    [TestFixture]
    public class MongoStorageTests
    {
        [Test]
        public void CanGetCollection()
        {
            InternalCanGetCollection(StorageFabric.GetStorageByConnectionString());
            InternalCanGetCollection(StorageFabric.GetStorageBySettings());
        }

        [Test]
        public void CanThrowExceptionIfThereNoCollectionNameAttribute()
        {
            InternalCanThrowExceptionIfThereNoCollectionNameAttribute(StorageFabric.GetStorageByConnectionString());
            InternalCanThrowExceptionIfThereNoCollectionNameAttribute(StorageFabric.GetStorageBySettings());
        }

        private static void InternalCanGetCollection(IMongoStorage mongoStorage)
        {
            var collection = mongoStorage.GetCollection<WithCollectionAttributeEntity, Guid>();
            Assert.NotNull(collection);
        }

        private static void InternalCanThrowExceptionIfThereNoCollectionNameAttribute(IMongoStorage mongoStorage)
        {
            Assert.Throws<ArgumentException>(() => { mongoStorage.GetCollection<WithoutCollectionAttributeEntity, Guid>(); });
        }
    }
}