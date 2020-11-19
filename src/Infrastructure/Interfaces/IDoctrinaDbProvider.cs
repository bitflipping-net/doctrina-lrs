namespace Doctrina.Infrastructure.Interfaces
{
    public interface IDoctrinaDbProvider
    {
        void Migrate();

        void Seed();
    }
}