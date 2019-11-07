using Doctrina.Application.About.Queries;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [Route("xapi/about")]
    [ApiController]
    [Produces("application/json")]
    public class AboutController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AboutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<About>> About()
        {
            return Ok(await _mediator.Send(new GetAboutQuery()));
        }
    }
}
