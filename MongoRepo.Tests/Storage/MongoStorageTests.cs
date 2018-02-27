using System;
using MongoRepo.Attributes;
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

        [Test]
        public void CanThrowExceptionIfCollectionNameAttributeEmpty()
        {
            InternalCanThrowExceptionIfCollectionNameAttributeEmpty(StorageFabric.GetStorageByConnectionString());
            InternalCanThrowExceptionIfCollectionNameAttributeEmpty(StorageFabric.GetStorageBySettings());
        }

        private static void InternalCanGetCollection(IMongoStorage mongoStorage)
        {
            var collection = mongoStorage.GetCollection<WithCollectionAttributeEntity, Guid>();
            Assert.NotNull(collection);
        }

        private static void InternalCanThrowExceptionIfThereNoCollectionNameAttribute(IMongoStorage mongoStorage)
        {
            Assert.Throws<ArgumentException>(
                () => { mongoStorage.GetCollection<WithoutCollectionAttributeEntity, Guid>(); },
                $"There is no {typeof(CollectionNameAttribute).Name} attribute at {typeof(WithEmptyCollectionAttributeEntity).Name}");
        }

        private static void InternalCanThrowExceptionIfCollectionNameAttributeEmpty(IMongoStorage mongoStorage)
        {
            Assert.Throws<ArgumentException>(
                () => { mongoStorage.GetCollection<WithEmptyCollectionAttributeEntity, Guid>(); },
                $"There is empty collection name at {typeof(CollectionNameAttribute).Name} in {typeof(WithEmptyCollectionAttributeEntity).Name}");
        }
    }
}