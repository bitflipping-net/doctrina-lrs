using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<ActivityEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityEntity> builder)
        {
            builder.ToTable("Activities");

            builder.HasKey(x => new { x.ActivityId, x.StoreId });

            builder.Property(a => a.StoreId)
                .IsRequired();

            builder.Property(x => x.ActivityId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.Id)
               .HasMaxLength(Constants.MAX_URL_LENGTH)
               .IsRequired();

            builder.Property(x => x.Hash)
                .HasMaxLength(Constants.SHA1_HASH_LENGTH)
                .IsRequired();

            builder.HasIndex(x => x.Hash)
                .IsUnique();
        }
    }
}
