using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ActivityDefinitionConfiguration : IEntityTypeConfiguration<ActivityDefinitionEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityDefinitionEntity> builder)
        {
            builder.ToTable("ActivityDefinitions");

            builder.Property(x => x.ActivityDefinitionId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.ActivityDefinitionId);

            builder.Property(e => e.Type);

            builder.Property(e => e.MoreInfo);

            //builder.Property(e => e.InteractionActivity);

            builder.Property(p => p.Names)
                .HasConversion(new JsonValueConverter<LanguageMapCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<LanguageMapCollection>(false));

            builder.Property(p => p.Descriptions)
                .HasConversion(new JsonValueConverter<LanguageMapCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<LanguageMapCollection>(false));

            builder.Property(p => p.Extensions)
                .HasConversion(new JsonValueConverter<ExtensionsCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<ExtensionsCollection>(false));
        }
    }
}
