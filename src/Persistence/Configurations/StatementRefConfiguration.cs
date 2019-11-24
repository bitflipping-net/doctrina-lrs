using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class StatementRefConfiguration : IEntityTypeConfiguration<StatementRefEntity>
    {
        public void Configure(EntityTypeBuilder<StatementRefEntity> builder)
        {
            builder.ToTable("StatementRefs");

            builder.Property(x => x.StatementRefId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.StatementRefId);

            builder.Property(x => x.StatementId)
                .IsRequired();
            builder.HasIndex(x => x.StatementId);
        }
    }
}
