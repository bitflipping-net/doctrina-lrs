using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<ActivityEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityEntity> builder)
        {
            builder.ToTable("Activities");

            builder.Property(x => x.ActivityId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.ActivityId);

            builder.Property(e => e.Id)
               .HasMaxLength(Constants.MAX_URL_LENGTH)
               .IsRequired();

            builder.Property(x => x.Hash)
                .HasMaxLength(Constants.HASH_LENGTH)
                .IsRequired();

            builder.HasOne(x => x.Definition);

            builder.HasIndex(x => x.Id)
               .IsUnique();

            builder.HasIndex(x => x.Hash)
                .IsUnique();
        }
    }
}
