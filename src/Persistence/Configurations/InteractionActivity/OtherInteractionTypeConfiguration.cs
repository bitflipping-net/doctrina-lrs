using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.InteractionActivities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class OtherInteractionTypeConfiguration : IEntityTypeConfiguration<OtherInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<OtherInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();
        }
    }
}
