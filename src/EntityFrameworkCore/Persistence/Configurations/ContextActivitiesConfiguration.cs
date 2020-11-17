using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctrina.Persistence.Configurations
{
    public class ContextActivitiesConfiguration : IEntityTypeConfiguration<ContextActivityEntity>
    {
        public void Configure(EntityTypeBuilder<ContextActivityEntity> builder)
        {
            builder.ToTable("ContextActivities");

            builder.Property(e => e.ContextId)
                .IsRequired();

            builder.HasKey(e => new { e.ContextId, e.ContextType, e.ActivityId });

            builder.Property(e => e.ContextType)
                .HasConversion(new EnumToStringConverter<ContextType>())
                .IsRequired();

            builder.HasOne(x => x.Activity)
                .WithMany()
                .HasForeignKey(x => x.ActivityId)
                .IsRequired();
        }
    }
}
