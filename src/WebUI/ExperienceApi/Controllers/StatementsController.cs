using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Models;
using Doctrina.Application.Statements.Queries;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Json;
using Doctrina.WebUI.ExperienceApi.Models;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using Doctrina.WebUI.Mvc.ModelBinders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
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

        public StatementsController(IMediator mediator, ILogger<StatementsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Order = 1)]
        [Produces("application/json", "multipart/mixed")]
        private async Task<IActionResult> GetStatement(
            [FromQuery]Guid statementId,
            [FromQuery(Name = "attachments")]bool includeAttachments = false,
            [FromQuery]ResultFormat format = ResultFormat.Exact)
        {
            Statement statement = await _mediator.Send(GetStatementQuery.Create(statementId, includeAttachments, format));

            if (statement == null)
                return NotFound();

            string statementJson = statement.ToJson(format);

            if (includeAttachments && statement.Attachments.Any(x => x.Payload != null))
            {
                var multipart = new MultipartContent("mixed")
                    {
                        new StringContent(statementJson, Encoding.UTF8, MediaTypes.Application.Json)
                    };
                foreach (var attachment in statement.Attachments)
                {
                    if (attachment.Payload != null)
                    {
                        var byteArrayContent = new ByteArrayContent(attachment.Payload);
                        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.ContentType);
                        byteArrayContent.Headers.Add(ApiHeaders.ContentTransferEncoding, "binary");
                        byteArrayContent.Headers.Add(ApiHeaders.XExperienceApiHash, attachment.SHA2);
                        multipart.Add(byteArrayContent);
                    }
                }
                var strMultipart = await multipart.ReadAsStringAsync();
                return Content(strMultipart, MediaTypes.Multipart.Mixed);
            }

            return Content(statementJson, MediaTypes.Application.Json);

        }

        [HttpGet(Order = 2)]
        [Produces("application/json", "multipart/mixed")]
        private async Task<IActionResult> GetVoidedStatement(
            [FromQuery]Guid voidedStatementId,
            [FromQuery(Name = "attachments")]bool includeAttachments = false,
            [FromQuery]ResultFormat format = ResultFormat.Exact)
        {
            Statement statement = await _mediator.Send(GetVoidedStatemetQuery.Create(voidedStatementId, includeAttachments, format));

            if (statement == null)
                return NotFound();

            string fullStatement = statement.ToJson(format);

            if (includeAttachments && statement.Attachments.Any(x => x.Payload != null))
            {
                var multipart = new MultipartContent("mixed")
                    {
                        new StringContent(fullStatement, Encoding.UTF8, MediaTypes.Application.Json)
                    };
                foreach (var attachment in statement.Attachments)
                {
                    if (attachment.Payload != null)
                    {
                        var byteArrayContent = new ByteArrayContent(attachment.Payload);
                        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.ContentType);
                        byteArrayContent.Headers.Add(ApiHeaders.ContentTransferEncoding, "binary");
                        byteArrayContent.Headers.Add(ApiHeaders.XExperienceApiHash, attachment.SHA2);
                        multipart.Add(byteArrayContent);
                    }
                }

                return Content(await multipart.ReadAsStringAsync(), MediaTypes.Multipart.Mixed);
            }

            return Content(fullStatement, MediaTypes.Application.Json);

        }

        /// <summary>
        /// Get statements
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD", Order = 3)]
        [Produces("application/json", "multipart/mixed")]
        public async Task<IActionResult> GetStatements([FromQuery]PagedStatementsQuery parameters)
        {
            ResultFormat format = parameters.Format ?? ResultFormat.Exact;

            // Validate parameters for combinations
            if (parameters.StatementId.HasValue || parameters.VoidedStatementId.HasValue)
            {
                var otherParameters = parameters.ToParameterMap(ApiVersion.GetLatest());
                otherParameters.Remove("attachments");
                otherParameters.Remove("format");
                if (otherParameters.Count > 0)
                {
                    return BadRequest("Only attachments and format parameters are allowed with using statementId or voidedStatementId");
                }

                bool attachments = parameters.Attachments.GetValueOrDefault();

                if (parameters.StatementId.HasValue)
                    return await GetStatement(
                        parameters.StatementId.Value,
                        attachments,
                        format);

                if (parameters.VoidedStatementId.HasValue)
                    return await GetVoidedStatement(
                        parameters.VoidedStatementId.Value,
                        attachments,
                        format);
            }

            StatementsResult result = new StatementsResult();
            PagedStatementsResult pagedResult = await _mediator.Send(parameters);

            // Derserialize to json statement object
            result.Statements = pagedResult.Statements;

            // Generate more url
            if (!string.IsNullOrEmpty(pagedResult.MoreToken))
            {
                result.More = new Uri(Url.Action("GetStatements") + $"?token={pagedResult.MoreToken}", UriKind.Relative);
            }

            if (parameters.Attachments.GetValueOrDefault())
            {
                // TODO: If the "attachment" property of a GET Statement is used and is set to true, the LRS MUST use the multipart response format and include all Attachments as described in Part Two.
                // Include attachment data, and return mutlipart/mixed
                return await MultipartResult(result, format, result.Statements);
            }

            return Content(result.ToJson(format), MediaTypes.Application.Json);
        }

        private async Task<IActionResult> MultipartResult(JsonModel result, ResultFormat format, ICollection<Statement> statements)
        {
            Response.ContentType = MediaTypes.Multipart.Mixed;
            var attachmentsWithPayload = statements.SelectMany(x => x.Attachments.Where(a => a.Payload != null));

            var multipart = new MultipartContent("mixed")
            {
                new StringContent(result.ToJson(format), Encoding.UTF8, MediaTypes.Application.Json)
            };

            foreach (var attachment in attachmentsWithPayload)
            {
                var byteArrayContent = new ByteArrayContent(attachment.Payload);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.ContentType);
                byteArrayContent.Headers.Add(ApiHeaders.ContentTransferEncoding, "binary");
                byteArrayContent.Headers.Add(ApiHeaders.XExperienceApiHash, attachment.SHA2);
                multipart.Add(byteArrayContent);
            }
            var strMultipartContent = await multipart.ReadAsStringAsync();
            return Content(strMultipartContent, MediaTypes.Multipart.Mixed);
        }

        /// <summary>
        /// Stores a single Statement with attachment(s) with the given id.
        /// </summary>
        /// <param name="statementId"></param>
        /// <param name="statement"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> PutStatement([FromQuery]Guid statementId, [ModelBinder(typeof(PutStatementModelBinder))]Statement statement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(PutStatementCommand.Create(statementId, statement));

            return NoContent();
        }

        /// <summary>
        /// Create statement(s) with attachment(s)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Array of Statement id(s) (UUID) in the same order as the corresponding stored Statements.</returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<Guid>>> PostStatements(PostStatementContent model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICollection<Guid> guids = await _mediator.Send(CreateStatementsCommand.Create(model.Statements));

            return Ok(guids);
        }
    }
}
