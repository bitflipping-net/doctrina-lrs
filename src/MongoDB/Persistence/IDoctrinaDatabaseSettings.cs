namespace Doctrina.MongoDB.Persistence {
    public interface IDoctrinaDatabaseSettings
    {
        string ConnectionString { get;  }
        string DatabaseName { get; }
    }
}