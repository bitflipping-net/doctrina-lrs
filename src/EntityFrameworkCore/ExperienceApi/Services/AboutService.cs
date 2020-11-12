using Doctrina.Application.About.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Services
{
    public class AboutService : IAboutService
    {
        private readonly IMediator mediator;

        public AboutService(IMediator mediator )
        {
            this.mediator = mediator;
        }

        public Task<About> GetAbout(CancellationToken cancellationToken = default) 
            => mediator.Send(new GetAboutQuery(), cancellationToken);
    }
}
