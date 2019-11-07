using Doctrina.Application.Agents.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [Authorize]
    [HeadWithoutBody]
    [RequiredVersionHeader]
    [Route("xapi/agents")]
    [Produces("application/json")]
    public class AgentsController : ApiControllerBase
    {
        private IMediator _mediator;

        public AgentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AcceptVerbs("GET", "HEAD")]
        public async Task<IActionResult> GetAgentProfile([FromQuery(Name = "agent")]string strAgent)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Agent agent = new Agent(strAgent);

                var person = await _mediator.Send(GetPersonCommand.Create(agent));
                if (person == null)
                    return NotFound();

                if (HttpMethods.IsHead(Request.Method))
                {
                    return NoContent();
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
