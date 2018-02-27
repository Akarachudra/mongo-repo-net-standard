using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    [CollectionName("WithCollectionAttributeEntity")]
    public class WithCollectionAttributeEntity : GuidEntity
    {
        public int SomeData { get; set; }
    }
}