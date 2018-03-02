using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Repository
{
    [CollectionName("ObjectIdTestEntity")]
    public class ObjectIdTestEntity : BaseObjectIdEntity
    {
        public int SomeData { get; set; }
    }
}