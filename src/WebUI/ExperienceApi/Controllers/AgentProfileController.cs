using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Personas.Commands;
using Doctrina.Application.Personas.Queries;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data;
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
            [BindRequired, FromQuery] string profileId,
            [BindRequired] Agent agent,
            CancellationToken cancellationToken = default)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaModel persona = await _mediator.Send(GetPersonaQuery.Create(agent), cancellationToken);
            if(persona == null)
                return NotFound();

            AgentProfileModel profile = await _mediator.Send(new GetAgentProfileQuery()
            {
                ProfileId = profileId,
                Persona = persona
            }, cancellationToken);

            if (profile == null)
                return NotFound();

            if (Request.TryConcurrencyCheck(profile?.Checksum, profile?.UpdatedAt, out int statusCode))
                return StatusCode(statusCode);

            var result = new FileContentResult(profile.Content, profile.ContentType)
            {
                LastModified = profile.UpdatedAt
            };
            Response.Headers.Add(HeaderNames.ETag, $"\"{profile.Checksum}\"");
            return result;
        }

        [HttpGet(Order = 2)]
        public async Task<ActionResult> GetAgentProfilesAsync(
            [BindRequired, FromQuery] Agent agent,
            [FromQuery] DateTimeOffset? since = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PersonaModel persona = await _mediator.Send(GetPersonaQuery.Create(agent), cancellationToken);
            if (persona == null)
                return Ok(Array.Empty<Guid>());

            ICollection<Domain.Models.Documents.AgentProfileModel> profiles = await _mediator.Send(GetAgentProfilesQuery.Create(persona, since), cancellationToken);

            if (profiles == null || profiles.Count == 0)
                return Ok(Array.Empty<Guid>());

            IEnumerable<string> ids = profiles.Select(x => x.ProfileId).ToList();

            string lastModified = profiles.OrderByDescending(x => x.UpdatedAt)
                .FirstOrDefault()?.UpdatedAt?.ToString("o");
            Response.Headers.Add(HeaderNames.LastModified, lastModified);
            return Ok(ids);
        }

        [HttpPut]
        [HttpPost]
        public async Task<ActionResult> SaveAgentProfileAsync(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Agent agent,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            [BindRequired, FromBody] byte[] content,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PersonaModel persona = await _mediator.Send(UpsertActorCommand.Create(agent), cancellationToken);
            AgentProfileModel profile = await _mediator.Send(GetAgentProfileQuery.Create(persona, profileId), cancellationToken);

            if (Request.TryConcurrencyCheck(profile?.Checksum, profile?.UpdatedAt, out int statusCode))
                return StatusCode(statusCode);

            if (profile == null)
            {
                profile = await _mediator.Send(
                    CreateAgentProfileCommand.Create(persona, profileId, content, contentType),
                    cancellationToken);
            }
            else
            {
                profile = await _mediator.Send(
                    UpdateAgentProfileCommand.Create(persona, profileId, content, contentType),
                    cancellationToken);
            }

            Response.Headers.Add(HeaderNames.ETag, $"\"{profile.Checksum}\"");
            Response.Headers.Add(HeaderNames.LastModified, profile.UpdatedAt?.ToString("o"));

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync(
            [BindRequired, FromQuery] string profileId,
            [BindRequired] Agent agent,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            CancellationToken cancelToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PersonaModel persona = await _mediator.Send(GetPersonaQuery.Create(agent), cancelToken);
            if (persona == null)
                return NotFound();

            AgentProfileModel profile = await _mediator.Send(GetAgentProfileQuery.Create(persona, profileId), cancelToken);

            if (Request.TryConcurrencyCheck(profile?.Checksum, profile?.UpdatedAt, out int statusCode))
                return StatusCode(statusCode);

            if (profile == null)
                return NotFound();

            await _mediator.Send(DeleteAgentProfileCommand.Create(profileId, persona), cancelToken);

            return NoContent();
        }
    }
}
