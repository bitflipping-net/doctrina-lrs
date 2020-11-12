using AutoMapper;
using Doctrina.Application.About.Queries;
using Doctrina.Application.Activities.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ActivityService(IMediator mediator, IMapper mapper)
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
