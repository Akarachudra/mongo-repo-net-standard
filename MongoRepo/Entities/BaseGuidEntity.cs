using System;

namespace MongoRepo.Entities
{
    public abstract class BaseGuidEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}