using AutoMapper;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Resources
{
    public class StatementResource : IStatementResource
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public StatementResource(IHttpContextAccessor httpContextAccessor, IMediator mediator, IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<Statement> GetStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken = default)
        {
            var stmt = await mediator.Send(StatementQuery.Create(statementId, attachments, format), cancellationToken);
            return mapper.Map<Statement>(stmt);
        }

        /// <inheritdoc/>
        public async Task<Statement> GetVoidedStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken = default)
        {
            var stmt = await mediator.Send(VoidedStatemetQuery.Create(statementId, attachments, format), cancellationToken);
            return mapper.Map<Statement>(stmt);
        }

        /// <inheritdoc/>
        public Task<ICollection<Guid>> PostStatements(StatementCollection statements, CancellationToken cancellationToken = default)
        {
            return mediator.Send(CreateStatementsCommand.Create(statements), cancellationToken);
        }

        /// <inheritdoc/>
        public Task PutStatement(Guid statementId, Statement statement, CancellationToken cancellationToken = default)
        {
            return mediator.Send(PutStatementCommand.Create(statementId, statement), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<StatementsResult> GetStatementsResult(StatementsQuery parameters, CancellationToken cancellationToken = default)
        {
            HttpRequest request = httpContextAccessor.HttpContext.Request;
            IQueryCollection requestQuery = request.Query;

            var cmd = new PagedStatementsQuery()
            {
                ActivityId = parameters.ActivityId,
                Agent = parameters.Agent,
                Ascending = parameters.Ascending,
                Attachments = parameters.Attachments,
                Format = parameters.Format,
                Registration = parameters.Registration,
                RelatedActivities = parameters.RelatedActivities,
                RelatedAgents = parameters.RelatedAgents,
                Limit = parameters.Limit,
                Since = parameters.Since,
                Until = parameters.Until,
                StatementId = parameters.StatementId,
                VerbId = parameters.VerbId,
                VoidedStatementId = parameters.VoidedStatementId
            };

            if (requestQuery.TryGetValue("cursor", out StringValues value))
            {
                cmd.Cursor = (string)value;
            }

            var pagedResult = await mediator.Send(cmd, cancellationToken);

            var result = new StatementsResult()
            {
                Statements = mapper.Map<StatementCollection>(pagedResult.Statements)
            };

            if (pagedResult.Cursor != null)
            {
                string cursor = pagedResult.Cursor;
                string relativePath = $"{request.Path}?cursor={cursor}";
                result.More = new Uri(relativePath, UriKind.Relative);
            }

            return result;
        }
    }
}
