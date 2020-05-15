using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ContextConfiguration : IEntityTypeConfiguration<ContextEntity>
    {
        public void Configure(EntityTypeBuilder<ContextEntity> builder)
        {
            builder.ToTable("Contexts");

            builder.HasKey(e => e.ContextId);
            builder.Property(e => e.ContextId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Extensions)
                .HasConversion(new ExtensionsCollectionValueConverter())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<ExtensionsCollection>(false));

            builder.HasOne(e => e.Instructor)
                .WithMany();

            builder.HasOne(e => e.Instructor)
                .WithMany();

            builder.HasOne(e => e.ContextActivities)
                .WithMany();
        }
    }
}
