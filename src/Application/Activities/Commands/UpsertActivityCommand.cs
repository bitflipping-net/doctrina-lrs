using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Activities.Commands
{
    public class UpsertActivityCommand : IRequest<ActivityModel>
    {
        public Activity Activity { get; set; }

        public static UpsertActivityCommand Create(Activity activity)
        {
            return new UpsertActivityCommand()
            {
                Activity = activity
            };
        }

        public static UpsertActivityCommand Create(Iri activityId)
        {
            return new UpsertActivityCommand()
            {
                Activity = new Activity()
                {
                    Id = activityId
                }
            };
        }
    }
}
