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
                    Server = new MongoServerAddress(StorageSettings.MongoServer, StorageSettings.MongoPort)
                },
                StorageSettings.MongoDataBaseName);
        }

        public static IMongoStorage GetStorageByConnectionString()
        {
            return new MongoStorage(StorageSettings.MongoConnectionString, StorageSettings.MongoDataBaseName);
        }
    }
}