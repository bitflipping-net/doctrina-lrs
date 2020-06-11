using Doctrina.Domain.Entities;

namespace Doctrina.Application.Tests.Common
{
    public static class ActivitiesTestFixture
    {
        public static ActivityEntity IdOnly() => new ActivityEntity()
        {
            Id = "http://www.example.com/activityId/hashset"
        };
    }
}