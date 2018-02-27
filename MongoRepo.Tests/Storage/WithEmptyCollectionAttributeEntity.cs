using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    [CollectionName("")]
    public class WithEmptyCollectionAttributeEntity : GuidEntity
    {
        public int SomeData { get; set; }
    }
}