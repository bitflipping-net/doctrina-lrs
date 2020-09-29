using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
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
                .HasConversion(new JsonValueConverter<InteractionComponentCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<InteractionComponentCollection>(false));
        }
    }
}
