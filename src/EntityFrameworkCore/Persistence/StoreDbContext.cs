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
            this._clientHttpContext = clientHttpContext;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<Client> Clients { get; set; }
        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<PersonaModel> Personas { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<StatementRelation> Relations { get; set; }
        public DbSet<StatementBaseModel> Statements { get; set; }
        //public DbSet<SubStatementEntity> SubStatements { get; set; }
        public DbSet<VerbModel> Verbs { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }

        public Guid StoreId => _clientHttpContext.GetClient().StoreId;

    }
}
