using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class PerformanceInteractionTypeConfiguration : IEntityTypeConfiguration<PerformanceInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<PerformanceInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Steps)
                .HasConversion(new InteractionComponentCollectionValueConverter())
                .HasColumnType("ntext");
        }
    }
}
