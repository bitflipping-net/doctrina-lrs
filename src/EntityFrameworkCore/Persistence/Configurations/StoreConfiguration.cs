using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(s => s.StoreId);
            builder.Property(store => store.StoreId)
                .ValueGeneratedOnAdd();

            builder.Property(store => store.Name)
                .HasMaxLength(Constants.MAX_NAME_LENGTH);

            builder.Property(store => store.StatementsCount);

            builder.Property(store => store.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(store => store.CreatedAt)
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Statements)
                .WithOne();

            builder.HasMany(store => store.Clients)
                .WithOne();

            builder.HasMany(store => store.Verbs)
                .WithOne();
        }
    }
}
