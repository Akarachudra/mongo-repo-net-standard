﻿namespace MongoRepo
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}