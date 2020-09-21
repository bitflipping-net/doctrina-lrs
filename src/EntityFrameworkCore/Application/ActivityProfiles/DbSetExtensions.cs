using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles
{
    public static class DbSetExtensions
    {
        public static Task<ActivityProfileEntity> GetProfileAsync(this DbSet<ActivityProfileEntity> query, Guid activityId, string profileId, Guid? registration, CancellationToken cancellationToken)
        {
            return query
                .Include(x => x.Activity)
                .FirstOrDefaultAsync(x =>
                    x.ActivityId == activityId &&
                    x.Key == profileId &&
                    x.RegistrationId == registration,
                    cancellationToken);
        }
    }
}
