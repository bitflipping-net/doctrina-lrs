using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Relations
{
    public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> builder)
        {
            builder.HasKey(clientScope => new { clientScope.ClientId, clientScope.ScopeId });

            builder.HasOne(client => client.Client)
                .WithMany()
                .HasForeignKey(clientScope => clientScope.ClientId);

            builder.HasOne(client => client.Scope)
                .WithMany()
                .HasForeignKey(clientScope => clientScope.ScopeId);
        }
    }
}
