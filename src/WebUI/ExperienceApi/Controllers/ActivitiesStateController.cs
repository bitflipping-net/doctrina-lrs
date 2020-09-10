﻿using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.ActivityStates.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    /// <summary>
    /// Generally, this is a scratch area for Learning Record Providers that do not have their own internal storage, or need to persist state across devices.
    /// </summary>
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/activities/state")]
    public class ActivitiesStateController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ActivitiesStateController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET|HEAD xapi/activities/state
        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetSingleState(
            [BindRequired, FromQuery] Iri activityId,
            [BindRequired, FromQuery] Agent agent,
            [FromQuery] string stateId = null,
            [FromQuery] DateTime? since = null,
            [FromQuery] Guid? registration = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AgentEntity agentEntity = await _mediator.Send(UpsertActorCommand.Create(agent));

            if (string.IsNullOrEmpty(stateId))
            {
                return await GetMutipleStates(
                    activityId,
                    agentEntity.AgentId,
                    registration,
                    since,
                    cancellationToken
                );
            }

            ActivityStateDocument stateDocument = await _mediator.Send(new GetActivityStateQuery()
            {
                StateId = stateId,
                ActivityId = activityId,
                AgentId = agentEntity.AgentId,
                Registration = registration
            }, cancellationToken);

            if (stateDocument == null)
            {
                return NotFound();
            }

            if (Request.TryConcurrencyCheck(stateDocument.Tag, stateDocument.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            var content = new FileContentResult(stateDocument.Content, stateDocument.ContentType.ToString())
            {
                LastModified = stateDocument.LastModified,
                EntityTag = new EntityTagHeaderValue($"\"{stateDocument.Tag}\"")
            };
            return content;
        }

        /// <summary>
        /// Fetches State ids of all state data for this context (Activity + Agent [ + registration if specified]). If "since" parameter is specified, this is limited to entries that have been stored or updated since the specified timestamp (exclusive).
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="strAgent"></param>
        /// <param name="stateId"></param>
        /// <param name="registration"></param>
        /// <returns></returns>
        private async Task<IActionResult> GetMutipleStates(
            Iri activityId,
            Guid agentId,
            Guid? registration = null,
            DateTime? since = null,
            CancellationToken cancellationToken = default)
        {
            ICollection<ActivityStateDocument> states = await _mediator.Send(new GetActivityStatesQuery()
            {
                ActivityId = activityId,
                AgentId = agentId,
                Registration = registration,
                Since = since
            }, cancellationToken);

            if (states.Count <= 0)
            {
                return Ok(Array.Empty<string>());
            }

            IEnumerable<string> ids = states.Select(x => x.StateId);
            string lastModified = states.OrderByDescending(x => x.LastModified)
                .FirstOrDefault()?.LastModified?.ToString("o");
            Response.Headers.Add("LastModified", lastModified);

            return Ok(ids);
        }

        // PUT|POST xapi/activities/state
        [HttpPut]
        [HttpPost]
        public async Task<IActionResult> PostSingleState(
            [BindRequired, FromQuery] string stateId,
            [BindRequired, FromQuery] Iri activityId,
            [BindRequired, FromQuery] Agent agent,
            [BindRequired, FromBody] byte[] body,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            [FromQuery] Guid? registration = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AgentEntity storedAgent = await _mediator.Send(UpsertActorCommand.Create(agent));
            ActivityEntity activity = (ActivityEntity)await _mediator.Send(UpsertActivityCommand.Create(activityId), cancellationToken);

            ActivityStateDocument stateDocument = await _mediator.Send(new GetActivityStateQuery()
            {
                StateId = stateId,
                ActivityId = activityId,
                AgentId = storedAgent.AgentId,
                Registration = registration
            }, cancellationToken);

            if (stateDocument != null)
            {
                stateDocument = await _mediator.Send(new UpdateStateDocumentCommand()
                {
                    StateId = stateId,
                    ActivityId = activityId,
                    AgentId = storedAgent.AgentId,
                    Content = body,
                    ContentType = contentType,
                    Registration = registration
                }, cancellationToken);
            }
            else
            {
                stateDocument = await _mediator.Send(new CreateStateDocumentCommand()
                {
                    StateId = stateId,
                    Activity = activity,
                    Agent = storedAgent,
                    Content = body,
                    ContentType = contentType,
                    Registration = registration
                }, cancellationToken);
            }

            Response.Headers.Add(HeaderNames.ETag, $"\"{stateDocument.Tag}\"");
            Response.Headers.Add(HeaderNames.LastModified, stateDocument.LastModified?.ToString("o"));

            return NoContent();
        }


        // DELETE xapi/activities/state
        [HttpDelete]
        public async Task<IActionResult> DeleteSingleState(
            [BindRequired, FromQuery] Iri activityId,
            [BindRequired, FromQuery] Agent agent,
            [FromQuery] string stateId = null,
            [FromQuery] Guid? registration = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agentEntity = (await _mediator.Send(UpsertActorCommand.Create(agent)));

            if (string.IsNullOrEmpty(stateId))
            {
                await _mediator.Send(new DeleteActivityStatesCommand()
                {
                    ActivityId = activityId,
                    AgentId = agentEntity.AgentId,
                    Registration = registration
                }, cancellationToken);

                return NoContent();
            }
            else
            {
                ActivityStateDocument stateDocument = await _mediator.Send(new GetActivityStateQuery()
                {
                    StateId = stateId,
                    ActivityId = activityId,
                    AgentId = agentEntity.AgentId,
                    Registration = registration
                }, cancellationToken);

                if (stateDocument == null)
                {
                    return NotFound();
                }

                await _mediator.Send(new DeleteActivityStateCommand()
                {
                    StateId = stateId,
                    ActivityId = activityId,
                    AgentId = agentEntity.AgentId,
                    Registration = registration
                }, cancellationToken);
            }

            return NoContent();
        }
    }
}
