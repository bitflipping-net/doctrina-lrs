using Doctrina.Application.Agents.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/agents")]
    [Produces("application/json")]
    public class AgentsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AgentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetAgentProfile(
            [BindRequired, FromQuery]Agent agent, 
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _mediator.Send(GetPersonCommand.Create(agent), cancellationToken);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}
