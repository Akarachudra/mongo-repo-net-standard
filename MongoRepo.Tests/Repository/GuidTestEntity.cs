using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Repository
{
    [CollectionName("GuidTestEntity")]
    public class GuidTestEntity : GuidEntity
    {
        public int SomeData { get; set; }
    }
}