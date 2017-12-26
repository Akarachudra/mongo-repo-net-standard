﻿using System;

namespace MongoRepo
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionNameAttribute : Attribute
    {
        public CollectionNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}