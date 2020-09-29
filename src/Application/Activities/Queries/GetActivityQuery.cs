using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Activities.Queries
{
    public class GetActivityQuery : IRequest<ActivityModel>
    {
        public Iri ActivityId { get; private set; }

        public static GetActivityQuery Create(Iri activityId)
        {
            return new GetActivityQuery()
            {
                ActivityId = activityId
            };
        }
    }
}
