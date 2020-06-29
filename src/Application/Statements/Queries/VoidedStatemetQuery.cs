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
    public class VoidedStatemetQuery : IRequest<StatementEntity>
    {
        public ResultFormat Format { get; private set; }
        public Guid VoidedStatementId { get; private set; }
        public bool IncludeAttachments { get; private set; }

        public static VoidedStatemetQuery Create(Guid voidedStatementId, bool includeAttachments, ResultFormat format)
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
