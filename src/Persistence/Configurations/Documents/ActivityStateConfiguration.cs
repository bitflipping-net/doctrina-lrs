using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class ActivityStateConfiguration : IEntityTypeConfiguration<ActivityStateEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityStateEntity> builder)
        {
            builder.ToTable("ActivityStates");

            builder.Property(e => e.ActivityStateId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.ActivityStateId);

            builder.Property(e => e.StateId)
                .IsRequired()
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.HasOne(e => e.Activity)
                .WithMany();

            builder.HasOne(e => e.Agent)
                .WithMany();

            builder.Property(x => x.Registration);

            //builder.HasIndex(e => new { e.StateId, e.Agent, e.Activity, e.Registration })
            //    .IsUnique();

            builder.OwnsOne(x => x.Document, a =>
            {
                a.Property(e => e.ContentType)
                    .HasMaxLength(255);

                a.Property(e => e.Content);

                a.Property(e => e.Checksum)
                    .IsRequired()
                    .HasMaxLength(50);

                a.Property(e => e.LastModified)
                    .HasDefaultValue(DateTimeOffset.UtcNow)
                    .IsRequired()
                    .ValueGeneratedOnAddOrUpdate();

                a.Property(e => e.CreateDate)
                    .ValueGeneratedOnAdd();
            });
        }
    }
}
