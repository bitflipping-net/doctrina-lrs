using Doctrina.Domain.Models;

namespace Doctrina.Application.Tests.Common
{
    public static class ActivitiesTestFixture
    {
        public static ActivityModel IdOnly() => new ActivityModel()
        {
            Iri = "http://www.example.com/activityId/hashset"
        };
    }
}