using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<ActivityModel>
    {
        public void Configure(EntityTypeBuilder<ActivityModel> builder)
        {
            builder.ToTable("Activities");

            builder.HasKey(x => new { x.ActivityId, x.StoreId });

            builder.Property(a => a.StoreId)
                .IsRequired();

            builder.Property(x => x.ActivityId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.Iri)
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
