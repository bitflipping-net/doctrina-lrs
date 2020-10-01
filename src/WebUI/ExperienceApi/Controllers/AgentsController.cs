using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Personas.Queries;
using Doctrina.Application.Persons.Queries;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            [BindRequired, FromQuery] Agent agent,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PersonaModel persona = await _mediator.Send(GetPersonaQuery.Create(agent), cancellationToken);
            PersonModel person = await _mediator.Send(GetPersonQuery.Create(persona), cancellationToken);

            if (person == null)
                return NotFound();

            return Ok(person);
        }
    }
}
