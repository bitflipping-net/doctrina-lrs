using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .IsRequired()
                .HasMaxLength(200);

            var valueComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            builder.Property(x => x.Scopes)
                .HasConversion(new JsonValueConverter<List<string>>())
                .Metadata
                .SetValueComparer(valueComparer);

            builder.Property(x => x.API)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Authority)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();
        }
    }
}
