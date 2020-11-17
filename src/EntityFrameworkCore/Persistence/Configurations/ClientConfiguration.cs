using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.Property(x => x.ClientId)
                .ValueGeneratedOnAdd();

            builder.HasKey(x => x.ClientId);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Scopes)
                .HasConversion(new JsonValueConverter<string[]>());

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.API)
                .IsRequired();
            builder.Property(x => x.Authority)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
