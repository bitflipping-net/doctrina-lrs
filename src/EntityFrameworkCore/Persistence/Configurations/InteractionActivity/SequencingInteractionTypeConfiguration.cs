using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class SequencingInteractionTypeConfiguration : IEntityTypeConfiguration<SequencingInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<SequencingInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Choices)
                .HasConversion(new JsonValueConverter<InteractionComponentCollection>())
                .HasColumnType("ntext")
                .HasColumnName("Choices")
                .Metadata
                .SetValueComparer(new ValueComparer<InteractionComponentCollection>(false));
        }
    }
}
