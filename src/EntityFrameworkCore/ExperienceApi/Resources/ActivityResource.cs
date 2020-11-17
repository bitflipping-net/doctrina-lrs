using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Resources
{
    public class ActivityResource : IActivityResource
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ActivityResource(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Activity> GetActivity(Iri activityId, CancellationToken cancellationToken)
        {
            var activityEntity = await mediator.Send(GetActivityQuery.Create(activityId), cancellationToken);
            return mapper.Map<Activity>(activityEntity);
        }
            
    }
}
