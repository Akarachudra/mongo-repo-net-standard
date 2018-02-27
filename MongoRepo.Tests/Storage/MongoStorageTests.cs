using System;
using MongoDB.Bson;
using MongoDB.Driver;
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

        [Test]
        public void CanDropCollection()
        {
            InternalCanDropCollection(StorageFabric.GetStorageByConnectionString());
            InternalCanDropCollection(StorageFabric.GetStorageBySettings());
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

        private static void InternalCanDropCollection(IMongoStorage mongoStorage)
        {
            var filter = new BsonDocument("name", StorageTestConstants.EntityCollectionName);
            try
            {
                mongoStorage.Database.CreateCollection(StorageTestConstants.EntityCollectionName);
            }
            catch (MongoCommandException)
            {
            }

            var collections = mongoStorage.Database.ListCollections(new ListCollectionsOptions { Filter = filter });
            Assert.IsTrue(collections.Any());
            mongoStorage.DropCollection<WithCollectionAttributeEntity, Guid>();
            collections = mongoStorage.Database.ListCollections(new ListCollectionsOptions { Filter = filter });
            Assert.IsFalse(collections.Any());
        }
    }
}