using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctrina.Persistence.Configurations
{
    public class ContextActivitiesConfiguration : IEntityTypeConfiguration<ContextActivity>
    {
        public void Configure(EntityTypeBuilder<ContextActivity> builder)
        {
            builder.HasKey(ac => new { ac.ContextId, ac.ContextType, ac.ActivityId });

            builder.HasOne(ac => ac.Context)
                .WithMany()
                .HasForeignKey(ac => ac.ContextId)
                .IsRequired();

            builder.HasOne(ac => ac.Activity)
                .WithMany()
                .HasForeignKey(ac => ac.ActivityId)
                .IsRequired();

            builder.Property(ac => ac.ContextType)
                .HasConversion(new EnumToStringConverter<ContextType>())
                .IsRequired();
        }
    }
}
