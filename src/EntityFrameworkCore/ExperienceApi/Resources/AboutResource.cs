using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Resources
{
    public class AboutResource : IAboutResource
    {
        public Task<About> GetAbout(CancellationToken cancellationToken = default)
        {
            var about = new About()
            {
                Version = ApiVersion.GetSupported().Select(x => x.Key)
            };

            return Task.FromResult(about);
        }
    }
}
