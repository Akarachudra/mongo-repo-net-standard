using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    [CollectionName(StorageTestConstants.EntityCollectionName)]
    public class WithCollectionAttributeEntity : GuidEntity
    {
        public int SomeData { get; set; }
    }
}