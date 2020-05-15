using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.ExperienceApi.Data.InteractionTypes;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Doctrina.Persistence.Configurations
{
    public class InteractionActivityBaseConfiguration : IEntityTypeConfiguration<InteractionActivityBase>
    {
        public void Configure(EntityTypeBuilder<InteractionActivityBase> builder)
        {
            builder.Property<Guid>("InteractionId")
                .ValueGeneratedOnAdd();
            builder.HasKey("InteractionId");

            builder.ToTable("InteractionActivities")
                .HasDiscriminator(x => x.InteractionType)
                .HasValue<ChoiceInteractionActivity>(InteractionType.Choice)
                .HasValue<FillInInteractionActivity>(InteractionType.FillIn)
                .HasValue<LongFillInInteractionActivity>(InteractionType.LongFillIn)
                .HasValue<MatchingInteractionActivity>(InteractionType.Matching)
                .HasValue<PerformanceInteractionActivity>(InteractionType.Performance)
                .HasValue<SequencingInteractionActivity>(InteractionType.Sequencing)
                .HasValue<TrueFalseInteractionActivity>(InteractionType.TrueFalse)
                .HasValue<LikertInteractionActivity>(InteractionType.Likert)
                .HasValue<OtherInteractionActivity>(InteractionType.Other);

            builder.Property(e => e.CorrectResponsesPattern)
                .HasConversion(new StringArrayValueConverter())
                .Metadata
                .SetValueComparer(new ValueComparer<ICollection<string>>(false));
        }
    }
}
