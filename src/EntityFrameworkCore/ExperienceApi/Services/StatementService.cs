using AutoMapper;
using Doctrina.Application.Statements.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Services
{
    public class StatementService : IStatementService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMediator medator;
        private readonly IMapper mapper;

        public StatementService(IHttpContextAccessor httpContextAccessor, IMediator medator, IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.medator = medator;
            this.mapper = mapper;
        }

        public Task<Statement> GetStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Statement> GetVoidedStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Task<ICollection<Guid>> PostStatements(StatementCollection statements, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task PutStatement(Guid statementId, Statement statement, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<StatementsResult> GetStatementsResult(StatementsQuery parameters, CancellationToken cancellationToken = default)
        {
            HttpRequest request = httpContextAccessor.HttpContext.Request;
            IQueryCollection requestQuery = request.Query;

            var cmd = mapper.Map<PagedStatementsQuery>(parameters);

            if (requestQuery.TryGetValue("cursor", out StringValues value))
            {
                cmd.Cursor = (string)value;
            }

            var pagedResult = await medator.Send(cmd, cancellationToken);

            var result = new StatementsResult()
            {
                Statements = mapper.Map<StatementCollection>(pagedResult.Statements)
            };

            if(pagedResult.Cursor != null)
            {
                string cursor = pagedResult.Cursor;
                string relativePath = $"{request.Path}?cursor={cursor}";
                result.More = new Uri(relativePath);
            }

            return result;
        }
    }
}
