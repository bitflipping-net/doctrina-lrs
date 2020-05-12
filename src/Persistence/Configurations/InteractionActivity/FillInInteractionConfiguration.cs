using Doctrina.Domain.Entities.InteractionActivities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class FillInInteractionTypeConfiguration : IEntityTypeConfiguration<FillInInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<FillInInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();
        }
    }
}
