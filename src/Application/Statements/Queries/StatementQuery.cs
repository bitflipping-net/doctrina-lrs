using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Statements.Queries
{
    /// <summary>
    /// Request a single statement
    /// </summary>
    public class StatementQuery : IRequest<StatementModel>
    {
        public Guid StatementId { get; set; }
        public bool IncludeAttachments { get; set; }
        public ResultFormat Format { get; set; }

        public static StatementQuery Create(Guid statementId, bool includeAttachments = false, ResultFormat format = ResultFormat.Exact)
        {
            return new StatementQuery()
            {
                StatementId = statementId,
                IncludeAttachments = includeAttachments,
                Format = format
            };
        }
    }
}
