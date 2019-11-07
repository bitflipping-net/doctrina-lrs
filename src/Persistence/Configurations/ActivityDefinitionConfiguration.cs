using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class ActivityDefinitionConfiguration : IEntityTypeConfiguration<ActivityDefinitionEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityDefinitionEntity> builder)
        {
            builder.ToTable("ActivityDefinitions");

            builder.Property(x => x.ActivityDefinitionId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.ActivityDefinitionId);

            builder.Property(e => e.Type);

            builder.Property(e => e.MoreInfo);

            builder.Property(p => p.Names)
                .HasConversion(new LanguageMapCollectionValueConverter())
                .HasColumnType("ntext");

            builder.Property(p => p.Descriptions)
                .HasConversion(new LanguageMapCollectionValueConverter())
                .HasColumnType("ntext");

            builder.Property(p => p.Extensions)
                .HasConversion(new ExtensionsCollectionValueConverter())
                .HasColumnType("ntext");
        }
    }
}
