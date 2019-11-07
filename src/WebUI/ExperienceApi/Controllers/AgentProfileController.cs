using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [Authorize]
    [HeadWithoutBody]
    [RequiredVersionHeader]
    [Route("xapi/agents/profile")]
    [Produces("application/json")]
    public class AgentProfileController : ApiControllerBase
    {
        private IMediator _mediator;

        public AgentProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AcceptVerbs("GET", "HEAD", Order = 1)]
        public async Task<ActionResult> GetAgentProfile(string profileId, [FromQuery(Name = "agent")]string strAgent)
        {
            // TODO: Parse 
            if (string.IsNullOrWhiteSpace(profileId))
            {
                throw new ArgumentNullException(nameof(profileId));
            }

            if (string.IsNullOrWhiteSpace(strAgent))
            {
                throw new ArgumentNullException("agent");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Agent agent = new Agent(strAgent);

            var profile = await _mediator.Send(new GetAgentProfileQuery()
            {
                ProfileId = profileId,
                Agent = agent
            });

            if (profile == null)
                return NotFound();

            string lastModified = profile.LastModified?.ToString("o");

            Response.ContentType = profile.ContentType;
            Response.Headers.Add("LastModified", lastModified);
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

            if (HttpMethods.IsHead(Request.Method))
            {
                return NoContent();
            }

            return new FileContentResult(profile.Content, profile.ContentType);
        }

        //[AcceptVerbs("GET", "HEAD")]
        [HttpGet(Order = 2)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAgentProfilesAsync([FromQuery(Name = "agent")]string strAgent, DateTimeOffset? since = null)
        {
            if (string.IsNullOrWhiteSpace(strAgent))
            {
                throw new ArgumentNullException("agent");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Agent agent = new Agent(strAgent);

            ICollection<AgentProfileDocument> profiles = await _mediator.Send(new GetAgentProfilesQuery(agent, since));

            if (profiles == null)
                return Ok(new Guid[] { });

            IEnumerable<string> ids = profiles.Select(x => x.ProfileId).ToList();

            string lastModified = profiles.OrderByDescending(x => x.LastModified)
                .FirstOrDefault()?
                .LastModified?
                .ToString("o");

            Response.Headers.Add("LastModified", lastModified);
            return Ok(ids);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="profileId"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        [AcceptVerbs("PUT", "POST")]
        public async Task<ActionResult> SaveAgentProfileAsync(string profileId, [FromQuery(Name = "agent")]string strAgent, [FromBody]byte[] content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string contentType = Request.ContentType;
            Agent agent = new Agent(strAgent);

            AgentProfileDocument profile = await _mediator.Send(new MergeAgentProfileCommand()
            {
                Agent = agent,
                ProfileId = profileId,
                Content = content,
                ContentType = contentType
            });

            Response.Headers.Add("ETag", $"\"{profile.Tag}\"");
            Response.Headers.Add("LastModified", profile.LastModified?.ToString("o"));

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync(string profileId, [FromQuery(Name = "agent")]string strAgent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string contentType = Request.ContentType;
            Agent agent = new Agent(strAgent);

            var profile = await _mediator.Send(GetAgentProfileQuery.Create(agent, profileId));
            if (profile == null)
                return NotFound();

            // TODO: Concurrency

            await _mediator.Send(new DeleteAgentProfileCommand()
            {
                ProfileId = profileId,
                Agent = agent
            });

            return NoContent();
        }
    }
}
