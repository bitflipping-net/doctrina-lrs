using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/agents/profile")]
    public class AgentProfileController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AgentProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Order = 1)]
        [HttpHead]
        public async Task<ActionResult> GetAgentProfile(
            [BindRequired, FromQuery]string profileId,
            [BindRequired]Agent agent,
            CancellationToken cancellationToken = default)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _mediator.Send(new GetAgentProfileQuery()
            {
                ProfileId = profileId,
                Agent = agent
            }, cancellationToken);

            if (profile == null)
            {
                return NotFound();
            }

            if(Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            var result = new FileContentResult(profile.Content, profile.ContentType)
            {
                LastModified = profile.LastModified
            };
            Response.Headers.Add(HeaderNames.ETag, $"\"{profile.Tag}\"");
            return result;
        }

        [HttpGet(Order = 2)]
        public async Task<ActionResult> GetAgentProfilesAsync(
            [BindRequired, FromQuery]Agent agent,
            [FromQuery]DateTimeOffset? since = null,
            CancellationToken cancelToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICollection<AgentProfileDocument> profiles = await _mediator.Send(new GetAgentProfilesQuery(agent, since), cancelToken);

            if (profiles == null)
            {
                return Ok(Array.Empty<Guid>());
            }

            IEnumerable<string> ids = profiles.Select(x => x.ProfileId).ToList();

            string lastModified = profiles.OrderByDescending(x => x.LastModified)
                .FirstOrDefault()?.LastModified?.ToString("o");
            Response.Headers.Add(HeaderNames.LastModified, lastModified);
            return Ok(ids);
        }

        [HttpPut]
        [HttpPost]
        public async Task<ActionResult> SaveAgentProfileAsync(
            [BindRequired, FromQuery]string profileId,
            [BindRequired, FromQuery]Agent agent,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            [BindRequired, FromBody]byte[] content,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AgentProfileDocument profile = await _mediator.Send(GetAgentProfileQuery.Create(agent, profileId), cancellationToken);

            if(Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            if(profile == null)
            {
                profile = await _mediator.Send(
                    CreateAgentProfileCommand.Create(agent, profileId, content, contentType),
                    cancellationToken);
            }
            else
            {
                profile = await _mediator.Send(
                    UpdateAgentProfileCommand.Create(agent, profileId, content, contentType),
                    cancellationToken);
            }

            Response.Headers.Add(HeaderNames.ETag, $"\"{profile.Tag}\"");
            Response.Headers.Add(HeaderNames.LastModified, profile.LastModified?.ToString("o"));

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync(
            [BindRequired, FromQuery]string profileId,
            [BindRequired]Agent agent,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            CancellationToken cancelToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _mediator.Send(GetAgentProfileQuery.Create(agent, profileId), cancelToken);

            if(Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            if (profile == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteAgentProfileCommand()
            {
                ProfileId = profileId,
                Agent = agent
            });

            return NoContent();
        }
    }
}
