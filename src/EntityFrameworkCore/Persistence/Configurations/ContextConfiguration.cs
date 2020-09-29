using Doctrina.Domain.Models;
using Doctrina.Domain.Models.ValueObjects;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ContextConfiguration : IEntityTypeConfiguration<StatementContext>
    {
        public void Configure(EntityTypeBuilder<StatementContext> builder)
        {
            builder.ToTable("Contexts");

            builder.HasKey(e => new { e.StoreId, e.ContextId });
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

            builder.Property(x => x.StoreId)
                .IsRequired();
        }
    }
}
