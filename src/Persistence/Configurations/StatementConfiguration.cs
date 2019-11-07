using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class StatementConfiguration : IEntityTypeConfiguration<StatementEntity>
    {
        public void Configure(EntityTypeBuilder<StatementEntity> builder)
        {
            builder.ToTable("Statements");

            builder.Property(p => p.StatementId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasKey(p => p.StatementId);

            // Actor
            builder.HasOne(e => e.Actor)
                .WithMany()
                .IsRequired();

            // Verb
            builder.HasOne(e => e.Verb)
                .WithMany()
                .IsRequired();

            builder.OwnsOne(p => p.Object);

            builder.HasOne(e => e.Result)
                .WithMany();

            builder.HasOne(e => e.Context)
               .WithMany();

            builder.Property(e => e.Timestamp)
                .IsRequired();

            builder.HasMany(x => x.Attachments)
                .WithOne();

            builder.Property(e => e.Stored)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(e => e.Version)
                .HasMaxLength(7);

            builder.HasOne(e => e.Authority)
                .WithMany();

            builder.Property(e => e.Voided)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
