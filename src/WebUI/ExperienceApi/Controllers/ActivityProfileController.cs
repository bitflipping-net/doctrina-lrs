using Doctrina.Application.ActivityProfiles.Commands;
using Doctrina.Application.ActivityProfiles.Queries;
using Doctrina.ExperienceApi.Data;
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
    [Route("xapi/activities/profile")]
    [Produces("application/json")]
    public class ActivityProfileController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ActivityProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <returns>200 OK, the Profile document</returns>
        [HttpGet(Order = 1)]
        [HttpHead(Order = 1)]
        public async Task<IActionResult> GetProfile(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Iri activityId,
            [FromQuery] Guid? registration = null,
            CancellationToken cancelToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _mediator.Send(new GetActivityProfileQuery()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Registration = registration
            }, cancelToken);

            if (profile == null)
            {
                return NotFound();
            }

            var result = new FileContentResult(profile.Content, profile.ContentType)
            {
                EntityTag = new EntityTagHeaderValue($"\"{profile.Checksum}\""),
                LastModified = profile.UpdatedAt
            };

            return result;
        }

        /// <summary>
        /// Fetches Profile ids of all Profile documents for an Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with these Profile documents.</param>
        /// <param name="since">Only ids of Profile documents stored since the specified Timestamp (exclusive) are returned.</param>
        /// <returns>200 OK, Array of Profile id(s)</returns>
        [HttpGet(Order = 2)]
        [HttpHead(Order = 2)]
        public async Task<ActionResult<string[]>> GetProfiles(
            [BindRequired, FromQuery] Iri activityId,
            [FromQuery] DateTimeOffset? since = null,
            CancellationToken cancelToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profiles = await _mediator.Send(GetActivityProfilesQuery.Create(activityId, since), cancelToken);

            if (profiles == null)
            {
                return Ok(Array.Empty<string>());
            }

            IEnumerable<string> ids = profiles.Select(x => x.Key);
            string lastModified = profiles.OrderByDescending(x => x.UpdatedAt)
                .FirstOrDefault()?.UpdatedAt.ToString("o");

            Response.Headers.Add("Last-Modified", lastModified);
            return Ok(ids);
        }

        /// <summary>
        /// Stores or changes the specified Profile document in the context of the specified Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <param name="document">The document to be stored or updated.</param>
        /// <returns>204 No Content</returns>
        [HttpPost]
        [HttpPut]
        public async Task<IActionResult> SaveProfile(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Iri activityId,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            [BindRequired, FromBody] byte[] body,
            [FromQuery] Guid? registration = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _mediator.Send(new GetActivityProfileQuery()
            {
                ActivityId = activityId,
                ProfileId = profileId,
                Registration = registration
            }, cancellationToken);

            if (Request.TryConcurrencyCheck(profile?.Checksum, profile?.UpdatedAt, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            if (profile != null)
            {
                // Optimistic Concurrency
                if (HttpMethods.IsPut(Request.Method))
                {
                    return Conflict();
                }

                await _mediator.Send(new UpdateActivityProfileCommand()
                {

                    ProfileId = profileId,
                    ActivityId = activityId,
                    Content = body,
                    ContentType = contentType,
                    Registration = registration
                }, cancellationToken);
            }
            else
            {
                await _mediator.Send(new CreateActivityProfileCommand()
                {
                    ProfileId = profileId,
                    ActivityId = activityId,
                    Content = body,
                    ContentType = contentType,
                    Registration = registration
                }, cancellationToken);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes the specified Profile document in the context of the specified Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <returns>204 No Content</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProfileAsync(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Iri activityId,
            [FromQuery] Guid? registration = null,
            CancellationToken cancelToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _mediator.Send(
                GetActivityProfileQuery.Create(activityId, profileId, registration),
                cancelToken);

            if (profile == null)
            {
                return NotFound();
            }

            if (Request.TryConcurrencyCheck(profile.Checksum, profile.UpdatedAt, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            await _mediator.Send(DeleteActivityProfileCommand.Create(
            profileId, activityId, registration), cancelToken);

            return NoContent();
        }
    }
}
