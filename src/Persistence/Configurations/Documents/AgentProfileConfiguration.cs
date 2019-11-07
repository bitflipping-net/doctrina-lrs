using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class AgentProfileConfiguration : IEntityTypeConfiguration<AgentProfileEntity>
    {
        public void Configure(EntityTypeBuilder<AgentProfileEntity> builder)
        {
            builder.ToTable("AgentProfiles");

            builder.Property(e => e.AgentProfileId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.AgentProfileId);

            builder.Property(e => e.ProfileId)
                .HasMaxLength(Constants.MAX_URL_LENGTH)
                .IsRequired();

            builder.HasIndex(e => e.ProfileId)
                .IsUnique();

            builder.HasOne(e => e.Agent)
                .WithMany();

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
