using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class StatementPayloadConfiguration : IEntityTypeConfiguration<StatementEncoded>
    {
        public void Configure(EntityTypeBuilder<StatementEncoded> builder)
        {

            builder.HasKey(p => new { p.StoreId, p.StatementId });
            builder.Property(p => p.StatementId)
                .IsRequired(true);

            builder.Property(p => p.StoreId)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasOne(x => x.Statement)
                .WithOne(x => x.Encoded);
        }
    }
}
