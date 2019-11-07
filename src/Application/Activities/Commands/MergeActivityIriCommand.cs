using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Activities.Commands
{
    public class MergeActivityIriCommand : IRequest<ActivityEntity>
    {
        public Iri ActivityId { get; set; }

        public static MergeActivityIriCommand Create(Iri activityId)
        {
            return new MergeActivityIriCommand()
            {
                ActivityId = activityId
            };
        }
    }
}
