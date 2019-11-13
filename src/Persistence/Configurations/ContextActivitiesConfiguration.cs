using Doctrina.Domain.Entities;
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

            builder.OwnsMany(e => e.Parent);

            builder.OwnsMany(e => e.Grouping);

            builder.OwnsMany(e => e.Category);

            builder.OwnsMany(e => e.Other);
        }
    }
}
