using Doctrina.Domain.Entities.InteractionActivities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class NumericInteractionTypeConfiguration : IEntityTypeConfiguration<NumericInteractionType>
    {
        public void Configure(EntityTypeBuilder<NumericInteractionType> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();
        }
    }
}
