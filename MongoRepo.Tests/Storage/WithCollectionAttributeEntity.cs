using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    [CollectionName(StorageTestConstants.EntityCollectionName)]
    public class WithCollectionAttributeEntity : BaseGuidEntity
    {
        public int SomeData { get; set; }
    }
}