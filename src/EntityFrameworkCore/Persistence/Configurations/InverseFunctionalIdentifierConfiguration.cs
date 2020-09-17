using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Persistence.Configurations
{
    public class InverseFunctionalIdentifierConfiguration : IEntityTypeConfiguration<InverseFuctionalIdentifier>
    {
        public void Configure(EntityTypeBuilder<InverseFuctionalIdentifier> builder)
        {
            builder.HasOne(x => x.Persona)
                .WithMany()
                .HasForeignKey(x=> x.PersonaId);

            builder.Property(x => x.Key);
            builder.Property(x => x.Value);
        }
    }
}
