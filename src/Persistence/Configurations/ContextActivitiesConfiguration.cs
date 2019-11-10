using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ContextActivitiesConfiguration : IEntityTypeConfiguration<ContextActivitiesEntity>
    {
        public void Configure(EntityTypeBuilder<ContextActivitiesEntity> builder)
        {
            builder.ToTable("ContextActivities");

            builder.Property(e => e.ContextActivitiesId)
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.ContextActivitiesId);

            builder.Property(e => e.Parent)
                .HasConversion(new ContextActivityCollectionValueConverter())
                .HasColumnType("ntext");

            builder.Property(e => e.Grouping)
                .HasConversion(new ContextActivityCollectionValueConverter())
                .HasColumnType("ntext");

            builder.Property(e => e.Category)
                 .HasConversion(new ContextActivityCollectionValueConverter())
                 .HasColumnType("ntext");

            builder.Property(e => e.Other)
                 .HasConversion(new ContextActivityCollectionValueConverter())
                 .HasColumnType("ntext");
        }
    }
}
