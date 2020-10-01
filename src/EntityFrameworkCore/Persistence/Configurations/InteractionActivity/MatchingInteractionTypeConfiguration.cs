using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class MatchingInteractionTypeConfiguration : IEntityTypeConfiguration<MatchingInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<MatchingInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Target)
                .HasConversion(new JsonValueConverter<InteractionComponentCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<InteractionComponentCollection>(false));

            builder.Property(x => x.Source)
                .HasConversion(new JsonValueConverter<InteractionComponentCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<InteractionComponentCollection>(false));
        }
    }
}
