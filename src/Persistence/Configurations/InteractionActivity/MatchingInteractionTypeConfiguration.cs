using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class MatchingInteractionTypeConfiguration : IEntityTypeConfiguration<MatchingInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<MatchingInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Target)
                .HasConversion(new InteractionComponentCollectionValueConverter())
                .HasColumnType("ntext");

            builder.Property(x => x.Source)
                  .HasConversion(new InteractionComponentCollectionValueConverter())
                  .HasColumnType("ntext");
        }
    }
}
