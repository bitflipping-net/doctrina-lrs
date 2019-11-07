using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.ActivityStates.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.WebUI.ExperienceApi.Models;
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
    /// <summary>
    /// Generally, this is a scratch area for Learning Record Providers that do not have their own internal storage, or need to persist state across devices.
    /// </summary>
    //[ApiAuthortize]
    //[ApiVersion]
    [Authorize]
    [HeadWithoutBody]
    [RequiredVersionHeaderAttribute]
    [Route("xapi/activities/state")]
    public class ActivitiesStateController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesStateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET|HEAD xapi/activities/state
        [AcceptVerbs("GET", "HEAD")]
        public async Task<ActionResult<StateDocumentModel>> GetSingleState(StateDocumentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                ActivityStateDocument stateDocument = await _mediator.Send(new GetActivityStateQuery()
                {
                    StateId = model.StateId,
                    ActivityId = model.ActivityId,
                    Agent = model.Agent,
                    Registration = model.Registration
                });

                if (stateDocument == null)
                    return NotFound();

                if (HttpMethods.IsHead(Request.Method))
                    return NoContent();

                var content = new FileContentResult(stateDocument.Content, stateDocument.ContentType.ToString());
                content.LastModified = stateDocument.LastModified;
                content.EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue(stateDocument.Tag);
                return content;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT|POST xapi/activities/state
        [ProducesResponseType(204)]
        [AcceptVerbs("PUT", "POST")]
        public async Task<IActionResult> PostSingleState(StateDocumentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _mediator.Send(new MergeStateDocumentCommand()
                {
                    StateId = model.StateId,
                    ActivityId = model.ActivityId,
                    Agent = model.Agent,
                    Registration = model.Registration,
                    Content = model.Content,
                    ContentType = model.ContentType
                });

                //var etag = EntityTagHeaderValue.Parse($"\"{document.Tag}\"");
                //Response.Headers.Add("ETag", etag.ToString());
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE xapi/activities/state
        [HttpDelete]
        public async Task<IActionResult> DeleteSingleState([FromQuery]StateDocumentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                ActivityStateDocument stateDocument = await _mediator.Send(new GetActivityStateQuery()
                {
                    StateId = model.StateId,
                    ActivityId = model.ActivityId,
                    Agent = model.Agent,
                    Registration = model.Registration
                });
                if (stateDocument == null)
                {
                    return NotFound();
                }

                await _mediator.Send(new DeleteActivityStateCommand()
                {
                    StateId = model.StateId,
                    ActivityId = model.ActivityId,
                    Agent = model.Agent,
                    Registration = model.Registration
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE xapi/activities/state
        [HttpDelete]
        public async Task<IActionResult> DeleteStatesAsync([FromQuery]Iri activityId, [FromQuery(Name = "agent")]string strAgent, [FromQuery]Guid? registration = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Agent agent = new Agent(strAgent);

            await _mediator.Send(new DeleteActivityStatesCommand()
            {
                ActivityId = activityId,
                Agent = agent,
                Registration = registration
            });

            return NoContent();
        }

        /// <summary>
        /// Fetches State ids of all state data for this context (Activity + Agent [ + registration if specified]). If "since" parameter is specified, this is limited to entries that have been stored or updated since the specified timestamp (exclusive).
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="strAgent"></param>
        /// <param name="stateId"></param>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMutipleStates(Iri activityId, [FromQuery(Name = "agent")]string strAgent, Guid? registration = null, DateTime? since = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Agent agent = new Agent(strAgent);

                ICollection<ActivityStateDocument> states = await _mediator.Send(new GetActivityStatesQuery()
                {
                    ActivityId = activityId,
                    Agent = agent,
                    Registration = registration,
                    Since = since
                });

                if (states.Count <= 0)
                {
                    return Ok(new string[0]);
                }

                IEnumerable<string> ids = states.Select(x => x.StateId);
                string lastModified = states.OrderByDescending(x => x.LastModified)
                    .FirstOrDefault()?
                    .LastModified?.ToString("o");
                Response.Headers.Add("LastModified", lastModified);

                return Ok(ids);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
