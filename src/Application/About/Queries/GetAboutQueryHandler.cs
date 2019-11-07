using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.About.Queries
{
    using Doctrina.ExperienceApi.Data;

    public class GetAboutQueryHandler : IRequestHandler<GetAboutQuery, About>
    {
        public Task<About> Handle(GetAboutQuery request, CancellationToken cancellationToken)
        {
            var about = new About()
            {
                Version = ApiVersion.GetSupported().Select(x => x.Key)
            };

            return Task.FromResult(about);
        }
    }
}
