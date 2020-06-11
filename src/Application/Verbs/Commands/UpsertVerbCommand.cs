using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Verbs.Commands
{
    public class UpsertVerbCommand : IRequest<VerbEntity>
    {
        public Verb Verb { get; set; }

        internal static UpsertVerbCommand Create(Verb verb)
        {
            return new UpsertVerbCommand()
            {
                Verb = verb
            };
        }
    }
}
