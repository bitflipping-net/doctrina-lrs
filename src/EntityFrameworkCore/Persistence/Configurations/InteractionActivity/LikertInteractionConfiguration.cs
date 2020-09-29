using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class LikertInteractionConfiguration : IEntityTypeConfiguration<LikertInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<LikertInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();

            builder.Property(x => x.Scale)
                .HasConversion(new InteractionComponentCollectionValueConverter())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<InteractionComponentCollection>(false));
        }
    }
}
