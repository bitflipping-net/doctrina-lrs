using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class SequencingInteractionTypeConfiguration : IEntityTypeConfiguration<SequencingInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<SequencingInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Choices)
                .HasConversion(new InteractionComponentCollectionValueConverter())
                .HasColumnType("ntext");
        }
    }
}
