using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    /// <summary>
    /// Request a single statement
    /// </summary>
    public class StatementQuery : IRequest<StatementEntity>
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
