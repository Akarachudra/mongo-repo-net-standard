﻿namespace MongoRepo.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}