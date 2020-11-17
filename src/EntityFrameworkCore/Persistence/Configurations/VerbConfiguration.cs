using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class VerbConfiguration : IEntityTypeConfiguration<VerbEntity>
    {
        public void Configure(EntityTypeBuilder<VerbEntity> builder)
        {
            builder.ToTable("Verbs");

            builder.Property(x => x.VerbId)
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.VerbId);

            builder.Property(e => e.Hash)
                .IsRequired()
                .HasMaxLength(Constants.SHA1_HASH_LENGTH);

            builder.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.Property(p => p.Display)
                .HasConversion(new JsonValueConverter<LanguageMapCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<LanguageMapCollection>(false));

            builder.HasIndex(x => x.Hash)
               .IsUnique();
        }
    }
}
