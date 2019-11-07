using Doctrina.Application.ActivityProfiles.Commands;
using Doctrina.Application.ActivityProfiles.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [Authorize]
    [HeadWithoutBody]
    [RequiredVersionHeaderAttribute]
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
        [AcceptVerbs("GET", "HEAD", Order = 1)]
        public async Task<IActionResult> GetProfile([BindRequired]string profileId, [BindRequired]Iri activityId, Guid? registration = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ActivityProfileDocument profile = await _mediator.Send(new GetActivityProfileQuery()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Registration = registration
            });

            if (profile == null)
                return NotFound();

            var result = new FileContentResult(profile.Content, profile.ContentType)
            {
                EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue(profile.Tag),
                LastModified = profile.LastModified
            };

            return Ok(result);
        }

        /// <summary>
        /// Fetches Profile ids of all Profile documents for an Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with these Profile documents.</param>
        /// <param name="since">Only ids of Profile documents stored since the specified Timestamp (exclusive) are returned.</param>
        /// <returns>200 OK, Array of Profile id(s)</returns>
        [AcceptVerbs("GET", "HEAD", Order = 2)]
        public async Task<ActionResult<string[]>> GetProfiles(Iri activityId, DateTimeOffset? since = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ICollection<ActivityProfileDocument> profiles = await _mediator.Send(new GetActivityProfilesQuery()
            {
                ActivityId = activityId,
                Since = since
            });

            if (profiles == null)
                return Ok(new string[0]);

            IEnumerable<string> ids = profiles.Select(x => x.ProfileId);
            string lastModified = profiles.OrderByDescending(x => x.LastModified)
                .FirstOrDefault()?
                .LastModified?.ToString("o");

            Response.Headers.Add("LastModified", lastModified);
            return Ok(ids);
        }

        /// <summary>
        /// Stores or changes the specified Profile document in the context of the specified Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <param name="document">The document to be stored or updated.</param>
        /// <returns>204 No Content</returns>
        [AcceptVerbs("PUT", "POST")]
        public async Task<IActionResult> SaveProfile([FromQuery]string profileId, [FromQuery]Iri activityId, [FromBody]byte[] document, [FromQuery]Guid? registration = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string contentType = Request.ContentType;

            ActivityProfileDocument profile = await _mediator.Send(new CreateActivityProfileCommand()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Content = document,
                ContentType = contentType,
                Registration = registration
            });

            Response.Headers["ETag"] = profile.Tag;

            return NoContent();
        }

        /// <summary>
        /// Deletes the specified Profile document in the context of the specified Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <returns>204 No Content</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProfileAsync(string profileId, Iri activityId, Guid? registration = null)
        {
            try
            {
                ActivityProfileDocument profile = await _mediator.Send(new GetActivityProfileQuery()
                {
                    ProfileId = profileId,
                    ActivityId = activityId,
                    Registration = registration
                });

                if (profile == null)
                    return NotFound();

                await _mediator.Send(new DeleteActivityProfileCommand()
                {
                    ProfileId = profileId,
                    ActivityId = activityId,
                    Registration = registration
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
