using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    [CollectionName("")]
    public class WithEmptyCollectionAttributeEntity : BaseGuidEntity
    {
        public int SomeData { get; set; }
    }
}