using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles
{
    public static class DbSetExtensions
    {
        public static Task<ActivityProfileEntity> GetProfileAsync(this DbSet<ActivityProfileEntity> query, Iri activityId, string profileId, Guid? registration, CancellationToken cancellationToken)
        {
            string activityHash = activityId.ComputeHash();
            return query
                .Include(x => x.Activity)
                .FirstOrDefaultAsync(x =>
                    x.Activity.Hash == activityHash &&
                    x.ProfileId == profileId &&
                    x.RegistrationId == registration,
                    cancellationToken);
        }
    }
}
