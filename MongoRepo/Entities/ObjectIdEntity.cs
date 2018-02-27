﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepo.Entities
{
    public abstract class ObjectIdEntity : IObjectIdEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}