using MongoDB.Driver;

namespace MongoRepo.Tests
{
    public static class StorageFabric
    {
        public static IMongoStorage GetStorageBySettings()
        {
            return new MongoStorage(
                new MongoClientSettings
                {
                    Server = new MongoServerAddress(TestSettings.MongoServer, TestSettings.MongoPort)
                },
                TestSettings.MongoDataBaseName);
        }

        public static IMongoStorage GetStorageByConnectionString()
        {
            return new MongoStorage(TestSettings.MongoConnectionString, TestSettings.MongoDataBaseName);
        }
    }
}