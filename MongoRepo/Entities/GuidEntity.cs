using System;

namespace MongoRepo.Entities
{
    public abstract class GuidEntity : IGuidEntity
    {
        public Guid Id { get; set; }
    }
}