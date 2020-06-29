namespace Doctrina.MongoDB.Persistence
{
    public class DoctrinaDatabaseSettings : IDoctrinaDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://doctrina:T1tg5MvxpvcfBupN@cluster0-9a7e7.azure.mongodb.net/doctrina?retryWrites=true&w=majority";
        public string DatabaseName { get; set; }
    }
}
