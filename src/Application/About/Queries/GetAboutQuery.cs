using MediatR;

namespace Doctrina.Application.About.Queries
{
    using Doctrina.ExperienceApi.Data;

    public class GetAboutQuery : IRequest<About>
    {
    }
}
