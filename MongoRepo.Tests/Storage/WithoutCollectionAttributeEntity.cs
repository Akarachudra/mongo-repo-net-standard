using MongoRepo.Entities;

namespace MongoRepo.Tests.Storage
{
    public class WithoutCollectionAttributeEntity : BaseGuidEntity
    {
        public int SomeData { get; set; }
    }
}