using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    [CollectionName("WithCollectionAttributeEntity")]
    public class WithCollectionAttributeEntity : BaseGuidEntity
    {
        public int SomeData { get; set; }
    }
}