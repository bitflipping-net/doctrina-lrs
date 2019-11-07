using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class LikertInteractionTypeConfiguration : IEntityTypeConfiguration<LikertInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<LikertInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Scale)
                .HasConversion(new InteractionComponentCollectionValueConverter())
                .HasColumnType("ntext");
        }
    }
}
