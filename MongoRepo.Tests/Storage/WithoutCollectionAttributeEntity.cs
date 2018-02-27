using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    public class WithoutCollectionAttributeEntity : GuidEntity
    {
        public int SomeData { get; set; }
    }
}