using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/activities")]
    public class ActivitiesController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ActivitiesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult> GetActivityDocumentAsync([FromQuery]GetActivityQuery command, CancellationToken cancelToken = default)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityEntity = await _mediator.Send(command, cancelToken);

            ResultFormat format = ResultFormat.Exact;
            if(!StringValues.IsNullOrEmpty(Request.Headers[HeaderNames.AcceptLanguage]))
            {
                format = ResultFormat.Canonical;
            }

            if (activityEntity == null)
            {
                return Ok(new Activity());
            }

            var activity = _mapper.Map<Activity>(activityEntity);

            return Ok(activity.ToJson(format));
        }
    }
}
