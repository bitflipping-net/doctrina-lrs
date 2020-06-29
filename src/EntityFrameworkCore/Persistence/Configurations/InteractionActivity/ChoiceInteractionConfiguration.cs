using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.EntityFrameworkCore.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class FillInInteractionConfiguration : IEntityTypeConfiguration<ChoiceInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<ChoiceInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Choices)
                .HasConversion(new InteractionComponentCollectionValueConverter())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<InteractionComponentCollection>(false));
        }
    }
}
