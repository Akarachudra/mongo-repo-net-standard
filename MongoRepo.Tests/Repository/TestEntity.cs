using MongoRepo.Attributes;
using MongoRepo.Entities;

namespace MongoRepo.Tests.Repository
{
    [CollectionName("TestCollection")]
    public class TestEntity : ObjectIdEntity
    {
        public int SomeData { get; set; }
    }
}