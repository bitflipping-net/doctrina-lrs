﻿using Doctrina.Domain.Models.InteractionActivities;
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
