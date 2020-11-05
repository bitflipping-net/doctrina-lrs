using Doctrina.Domain.Models;
using Doctrina.Domain.Models.ValueObjects;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ContextConfiguration : IEntityTypeConfiguration<ContextModel>
    {
        public void Configure(EntityTypeBuilder<ContextModel> builder)
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
                .WithMany()
                .HasForeignKey(e=> e.TeamId);

            builder.HasOne(e => e.Team)
                .WithMany()
                .HasForeignKey(e => e.InstructorId);

            builder.HasMany(e => e.ContextActivities)
                .WithOne()
                .HasForeignKey(x=> x.ContextId);
        }
    }
}
