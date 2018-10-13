using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Repository
{
    [CollectionName("ObjectIdTestEntity")]
    public class ObjectIdTestEntity : BaseEntity
    {
        public int SomeData { get; set; }
    }
}