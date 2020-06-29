using Doctrina.Domain.Entities;
using MongoDB.Bson.Serialization;
using System;

namespace Doctrina.MongoDB.Persistence.Configuration
{
    public abstract class AbstractClassMap<TClass>
    {
        public BsonClassMap<TClass> ClassMap;

        public AbstractClassMap()
        {

            if (!BsonClassMap.IsClassMapRegistered(typeof(TClass)))
            {
                BsonClassMap.RegisterClassMap<TClass>(RegisterClassMap);
            }
        }

        public abstract void RegisterClassMap(BsonClassMap<TClass> classMap);
    }
}