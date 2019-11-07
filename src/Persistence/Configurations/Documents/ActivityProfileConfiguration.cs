using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class ActivityProfileConfiguration : IEntityTypeConfiguration<ActivityProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityProfileEntity> builder)
        {
            builder.ToTable("ActivityProfiles");

            builder.Property(x => x.ActivityProfileId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.ActivityProfileId);

            builder.Property(e => e.ProfileId)
                .IsRequired();

            builder.HasOne(e => e.Activity)
                .WithMany();

            //builder.HasIndex(e => new { e.ProfileId, e.ActivityId })
            //   .IsUnique();

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
