using Doctrina.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Doctrina.MongoDB.Persistence.Configuration
{
    public class StatementClassMap : AbstractClassMap<StatementEntity>
    {
        public StatementClassMap()
        {
            BsonClassMap.RegisterClassMap<StatementEntity>(cm =>
            {
                cm.MapIdMember(s=> s.StatementId);
            });
        }

        public override void RegisterClassMap(BsonClassMap<StatementEntity> classMap)
        {
            classMap.MapIdMember(s=> s.StatementId);
            classMap.UnmapMember(s => s.FullStatement);
            classMap.AutoMap();
        }
    }
}