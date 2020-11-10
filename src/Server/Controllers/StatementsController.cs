using AutoMapper;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Models;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.Server.Mvc.ActionResults;
using Doctrina.Server.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Queries = Doctrina.Application.Statements.Queries;

namespace Doctrina.Server.Controllers
{
    /// <summary>
    /// The basic communication mechanism of the Experience API.
    /// </summary>
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/statements")]
    [Produces("application/json")]
    public class StatementsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StatementsController> _logger;
        private readonly IMapper _mapper;

        public StatementsController(IMediator mediator, ILogger<StatementsController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get statements
        /// </summary>
        [HttpGet(Order = 3)]
        [HttpHead]
        [Produces("application/json", "multipart/mixed")]
        public async Task<IActionResult> GetStatements([FromQuery] Queries.PagedStatementsQuery parameters, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResultFormat format = parameters.Format ?? ResultFormat.Exact;
            if (!StringValues.IsNullOrEmpty(Request.Headers[HeaderNames.AcceptLanguage]))
            {
                format = ResultFormat.Canonical;
            }

            bool attachments = parameters.Attachments.GetValueOrDefault();

            if (parameters.StatementId.HasValue || parameters.VoidedStatementId.HasValue)
            {
                IRequest<StatementEntity> requestQuery = null;
                if (parameters.StatementId.HasValue)
                {
                    Guid statementId = parameters.StatementId.Value;
                    requestQuery = Queries.StatementQuery.Create(statementId, attachments, format);
                }
                else if (parameters.VoidedStatementId.HasValue)
                {
                    Guid voidedStatementId = parameters.VoidedStatementId.Value;
                    requestQuery = Queries.VoidedStatemetQuery.Create(voidedStatementId, attachments, format);
                }

                StatementEntity statementEntity = await _mediator.Send(requestQuery, cancellationToken);

                if (statementEntity == null)
                {
                    return NotFound();
                }

                var statement = _mapper.Map<Statement>(statementEntity);

                return new StatementActionResult(statement, format);
            }

            PagedStatementsResult pagedResult = await _mediator.Send(parameters, cancellationToken);
            var mappedStatements = _mapper.Map<IEnumerable<Statement>>(pagedResult.Statements);
            StatementsResult result = new StatementsResult()
            {
                Statements = new StatementCollection(mappedStatements)
            };

            // Generate more url
            if (!string.IsNullOrEmpty(pagedResult.MoreToken))
            {
                result.More = new Uri($"/xapi/statements?more={pagedResult.MoreToken}", UriKind.Relative);
            }

            return new StatementsActionResult(result, format, attachments);
        }

        /// <summary>
        /// Stores a single Statement with attachment(s) with the given id.
        /// </summary>
        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> PutStatement([FromQuery] Guid statementId, Statement statement, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(PutStatementCommand.Create(statementId, statement), cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Create statement(s) with attachment(s)
        /// </summary>
        /// <returns>Array of Statement id(s) (UUID) in the same order as the corresponding stored Statements.</returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<Guid>>> PostStatements(StatementCollection statements, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICollection<Guid> guids = await _mediator.Send(CreateStatementsCommand.Create(statements), cancellationToken);

            return Ok(guids);
        }
    }
}
