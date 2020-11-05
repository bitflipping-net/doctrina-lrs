using System;
using Doctrina.Application.Common;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.Domain.Models.Relations;
using Doctrina.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Persistence
{
    public class StoreDbContext: DbContext, IStoreDbContext
    {
        private readonly IClientHttpContext _clientHttpContext;

        public StoreDbContext(DbContextOptions<StoreDbContext> options, IClientHttpContext clientHttpContext)
           : base(options)
        {
            _clientHttpContext = clientHttpContext;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<Scope> Scopes { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<PersonModel> Persons { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<PersonaModel> Personas { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<DocumentModel> Documents { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<StatementRelation> Relations { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<StatementBaseModel> Statements { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<VerbModel> Verbs { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<ActivityModel> Activities { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Guid StoreId => _clientHttpContext.GetClient().StoreId;

    }
}
