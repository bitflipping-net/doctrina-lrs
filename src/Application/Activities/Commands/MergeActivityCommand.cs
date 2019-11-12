using System;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Activities.Commands
{
    public class MergeActivityCommand : IRequest<IActivityEntity>
    {
        public IActivity Activity { get; set; }

        public static MergeActivityCommand Create(IActivity activity)
        {
            return new MergeActivityCommand()
            {
                Activity = activity
            };
        }

        internal static MergeActivityCommand Create(Iri activityId)
        {
            return new MergeActivityCommand()
            {
                Activity = new Activity()
                {
                    Id = activityId
                }
            };
        }
    }
}
