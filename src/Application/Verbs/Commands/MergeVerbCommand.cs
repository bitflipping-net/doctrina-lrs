using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Verbs.Commands
{
    public class MergeVerbCommand : IRequest<VerbEntity>
    {
        public VerbEntity Verb { get; set; }

        public class Handler : IRequestHandler<MergeVerbCommand, VerbEntity>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IDoctrinaDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<VerbEntity> Handle(MergeVerbCommand request, CancellationToken cancellationToken)
            {
                string verbHash = request.Verb.Hash ?? new Iri(request.Verb.Id).ComputeHash();

                var verb = await _context.Verbs.FirstOrDefaultAsync(x=> x.Hash == verbHash, cancellationToken);
                if (verb != null)
                {
                    // TODO: Update verb Display language maps
                    foreach (var dis in request.Verb.Display)
                    {
                        if (verb.Display.ContainsKey(dis.Key))
                        {
                            verb.Display[dis.Key] = dis.Value;
                        }
                        else
                        {
                            verb.Display.Add(dis);
                        }
                    }
                }
                else
                {
                    verb = request.Verb;
                    verb.Hash = verbHash;

                    _context.Verbs.Add(verb);
                }

                return verb;
            }
        }

        internal static MergeVerbCommand Create(VerbEntity verb)
        {
            return new MergeVerbCommand()
            {
                Verb = verb
            };
        }
    }
}
