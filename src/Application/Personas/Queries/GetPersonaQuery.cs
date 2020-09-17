using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Personas.Queries
{
    public class GetPersonaQuery : IRequest<Person>
    {
        public static GetPersonaQuery Create()
        {
            return new GetPersonaQuery();
        }
    }
}
