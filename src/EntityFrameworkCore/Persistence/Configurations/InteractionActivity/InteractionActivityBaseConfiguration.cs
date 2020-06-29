using Doctrina.Domain.Entities.InteractionActivities;
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
                .HasValue<ChoiceInteractionActivity>("choice")
                .HasValue<FillInInteractionActivity>("fill-in")
                .HasValue<LongFillInInteractionActivity>("long-fill-in")
                .HasValue<MatchingInteractionActivity>("matching")
                .HasValue<PerformanceInteractionActivity>("performance")
                .HasValue<SequencingInteractionActivity>("sequencing")
                .HasValue<TrueFalseInteractionActivity>("true-false")
                .HasValue<LikertInteractionActivity>("likert")
                .HasValue<OtherInteractionActivity>("other");

            builder.Property(e => e.CorrectResponsesPattern)
                .HasConversion(new StringArrayValueConverter())
                .Metadata
                .SetValueComparer(new ValueComparer<ICollection<string>>(false));
        }
    }
}
