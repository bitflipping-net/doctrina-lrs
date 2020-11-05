using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(client => client.ClientId);

            builder.Property(client => client.Authority);

            builder.Property(client => client.CreatedAt)
                .ValueGeneratedOnAdd();

            builder.Property(client => client.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(client => client.Scopes)
                .WithOne();

            builder.HasOne(client => client.Store)
                .WithMany()
                .HasForeignKey(client=> client.StoreId);
        }
    }
}
