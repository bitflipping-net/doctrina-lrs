using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class VerbConfiguration : IEntityTypeConfiguration<VerbEntity>
    {
        public void Configure(EntityTypeBuilder<VerbEntity> builder)
        {
            builder.ToTable("Verbs");

            builder.Property(x => x.VerbId)
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.VerbId);

            builder.Property(e => e.Hash)
                .IsRequired()
                .HasMaxLength(Constants.HASH_LENGTH);

            builder.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.Property(p => p.Display)
                .HasConversion(new LanguageMapCollectionValueConverter())
                .HasColumnType("ntext");

            builder.HasIndex(x => x.Hash)
               .IsUnique();

            builder.HasIndex(x => x.Id)
               .IsUnique();
        }
    }
}
