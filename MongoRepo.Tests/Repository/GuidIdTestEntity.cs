using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Repository
{
    [CollectionName("GuidIdTestEntity")]
    public class GuidIdTestEntity : BaseGuidEntity
    {
        public int SomeData { get; set; }

        public int AnotherData { get; set; }
    }
}