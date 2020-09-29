using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KnownVerbs = Doctrina.ExperienceApi.Data.KnownVerbs;

namespace Doctrina.Application.Statements.Notifications
{
    public class VoidStatementHandler : INotificationHandler<StatementCreated>
    {
        private readonly string VoidedHash = KnownVerbs.Voided.ComputeHash();
        private readonly IStoreDbContext _context;

        public VoidStatementHandler(IStoreDbContext context)
        {
            _context = context;
        }

        public async Task Handle(StatementCreated notification, CancellationToken cancellationToken)
        {
            Guid storeId = _context.StoreId;
            StatementModel model = notification.Model;
            if (model.Verb.Hash == VoidedHash)
            {
                if (model.ObjectType == Domain.Models.ObjectType.StatementRef)
                {
                    Guid statementId = model.ObjectId;
                    StatementModel statement = await _context.Statements
                        .OfType<StatementModel>()
                        .Include(x => x.Verb)
                        .FirstOrDefaultAsync(x => x.StatementId == statementId && x.StoreId == storeId, cancellationToken);

                    if (statement != null
                        && statement.Verb.Hash != VoidedHash)
                    {
                        statement.VoidingStatement = model;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }
            else
            {
                // Detect if current statement has already been voided
                StatementModel voidingStatement = await _context.Statements.OfType<StatementModel>().SingleOrDefaultAsync(x =>
                    x.ObjectType == Domain.Models.ObjectType.StatementRef 
                    && x.ObjectId == model.Id
                    && x.Verb.Hash == VoidedHash
                    && x.StoreId == storeId,
                    cancellationToken
                );

                if (voidingStatement != null)
                {
                    model.VoidingStatement = model;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
