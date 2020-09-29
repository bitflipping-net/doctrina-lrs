using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Statements.Queries
{
    public class VoidedStatemetQuery : IRequest<StatementModel>
    {
        public ResultFormat Format { get; private set; }
        public Guid VoidedStatementId { get; private set; }
        public bool IncludeAttachments { get; private set; }

        public static VoidedStatemetQuery Create(Guid voidedStatementId, bool includeAttachments = false, ResultFormat format = ResultFormat.Exact)
        {
            return new VoidedStatemetQuery()
            {
                VoidedStatementId = voidedStatementId,
                IncludeAttachments = includeAttachments,
                Format = format
            };
        }
    }
}
