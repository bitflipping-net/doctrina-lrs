﻿using Doctrina.Domain.Models.InteractionActivities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Interactions
{
    public class LongFillInInteractionConfiguration : IEntityTypeConfiguration<LongFillInInteractionActivity>
    {
        public void Configure(EntityTypeBuilder<LongFillInInteractionActivity> builder)
        {
            builder.HasBaseType<InteractionActivityBase>();
        }
    }
}
