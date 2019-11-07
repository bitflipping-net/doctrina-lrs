using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class SubStatementConfiguration : IEntityTypeConfiguration<SubStatementEntity>
    {
        public void Configure(EntityTypeBuilder<SubStatementEntity> builder)
        {
            builder.ToTable("SubStatements");

            // Create ID as shadow prop
            builder.Property<Guid>("SubStatementId")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasKey("SubStatementId");

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

            //builder.OwnsMany(e => e.Attachments, a =>
            //{
            //    a.Property<Guid>("AttachmentId");
            //    a.HasKey("AttachmentId");
            //    a.WithOwner()
            //        .HasForeignKey("StatementId");

            //    a.Property(e => e.UsageType)
            //    .IsRequired()
            //    .HasMaxLength(Constants.MAX_URL_LENGTH);

            //    a.Property(e => e.ContentType)
            //        .IsRequired()
            //        .HasMaxLength(255);

            //    a.Property(e => e.SHA2)
            //        .IsRequired();

            //    a.Property(e => e.Display)
            //        .HasConversion(new LanguageMapCollectionValueConverter())
            //        .HasColumnType("ntext");

            //    a.Property(e => e.Description)
            //      .HasConversion(new LanguageMapCollectionValueConverter())
            //      .HasColumnType("ntext");

            //    a.Property(e => e.Length)
            //        .IsRequired();

            //    a.ToTable("SubStatement_Attachments");
            //});
        }
    }
}
