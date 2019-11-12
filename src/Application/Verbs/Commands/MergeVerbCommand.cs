using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Verbs.Commands
{
    public class MergeVerbCommand : IRequest<IVerbEntity>
    {
        public IVerb Verb { get; set; }

        internal static MergeVerbCommand Create(IVerb verb)
        {
            return new MergeVerbCommand()
            {
                Verb = verb
            };
        }
    }
}
