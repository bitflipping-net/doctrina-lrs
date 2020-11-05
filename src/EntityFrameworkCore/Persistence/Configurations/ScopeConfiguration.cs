using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ScopeConfiguration : IEntityTypeConfiguration<Scope>
    {
        public void Configure(EntityTypeBuilder<Scope> builder)
        {
            builder.HasKey(x => x.Key);

            builder.Property(scope => scope.Description)
                .IsRequired();

            builder.Property(scope => scope.Name)
                .IsRequired();
        }
    }
}
