using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Verbs.Commands
{
    public class MergeVerbCommandHandler : IRequestHandler<MergeVerbCommand, IVerbEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MergeVerbCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IVerbEntity> Handle(MergeVerbCommand request, CancellationToken cancellationToken)
        {
            string verbHash = request.Verb.Id.ComputeHash();

            var verb = await _context.Verbs.FirstOrDefaultAsync(x => x.Hash == verbHash, cancellationToken);
            if (verb != null)
            {
                // TODO: Update verb Display language maps
                // We MAY rollback any changes 
                if(request.Verb.Display != null)
                {
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
            }
            else
            {
                verb = _mapper.Map<VerbEntity>(request.Verb);
                verb.Hash = verbHash;

                _context.Verbs.Add(verb);
            }

            return verb;
        }
    }
}
