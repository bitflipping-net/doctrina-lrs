using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Doctrina.MongoDB.Persistence
{
    public interface IDoctrinaDbContext
    {
        IMongoCollection<StatementEntity> Statements { get; }
        IMongoCollection<AgentEntity> Agents { get; }
        IMongoCollection<ActivityEntity> Activities { get; }
        IMongoCollection<VerbEntity> Verbs { get; }
        IMongoCollection<AgentProfileEntity> AgentProfiles { get; }
        IMongoCollection<ActivityProfileEntity> ActivityProfiles { get; }
        IMongoCollection<ActivityStateEntity> ActivityStates { get; }
    }
}