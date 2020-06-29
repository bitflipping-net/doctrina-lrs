using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Doctrina.MongoDB.Persistence
{
    public class DoctrinaDbContext : IDoctrinaDbContext
    {
        private readonly IDoctrinaDatabaseSettings _dbSettings;
        private MongoClient _client;
        private readonly IMongoDatabase _db;

        public DoctrinaDbContext(IDoctrinaDatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
            _client = new MongoClient(dbSettings.ConnectionString);
            _db = _client.GetDatabase(dbSettings.DatabaseName);
        }

        public IMongoCollection<StatementEntity> Statements => _db.GetCollection<StatementEntity>("Statements");
        public IMongoCollection<AgentEntity> Agents => _db.GetCollection<AgentEntity>("Agents");
        public IMongoCollection<ActivityEntity> Activities => _db.GetCollection<ActivityEntity>("Activities");
        public IMongoCollection<VerbEntity> Verbs => _db.GetCollection<VerbEntity>("Verbs");
        public IMongoCollection<AgentProfileEntity> AgentProfiles => _db.GetCollection<AgentProfileEntity>("AgentProfiles");
        public IMongoCollection<ActivityProfileEntity> ActivityProfiles => _db.GetCollection<ActivityProfileEntity>("ActivityProfiles");
        public IMongoCollection<ActivityStateEntity> ActivityStates => _db.GetCollection<ActivityStateEntity>("ActivityStates");
    }
}