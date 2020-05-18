using AutoMapper;
using Doctrina.Application.Common.Exceptions;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Data.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class UpdateStateDocumentHandler : IRequestHandler<UpdateStateDocumentCommand, ActivityStateDocument>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateStateDocumentHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActivityStateDocument> Handle(UpdateStateDocumentCommand request, CancellationToken cancellationToken)
        {
            AgentEntity agent = _mapper.Map<AgentEntity>(request.Agent);
            string activityHash = request.ActivityId.ComputeHash();
            var query = _context.ActivityStates
                .Where(x=> x.StateId == request.StateId)
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.Hash == agent.Hash);

            if (request.Registration.HasValue)
            {
                query.Where(x => x.Registration == request.Registration);
            }

            ActivityStateEntity state = await query.SingleOrDefaultAsync(cancellationToken);

            if (state == null)
            {
                throw new NotFoundException("State", request.StateId);
            }

            var stateDocument = state.Document;
            if(stateDocument.ContentType != MediaTypes.Application.Json
            || request.ContentType != MediaTypes.Application.Json)
            {
                throw new BadRequestException();
            }

            JsonString jsonString = Encoding.UTF8.GetString(request.Content);
            JsonString savedJsonString = Encoding.UTF8.GetString(stateDocument.Content);
            jsonString.Merge(savedJsonString);

            byte[] mergedJsonBytes = Encoding.UTF8.GetBytes(jsonString.ToString());

            state.Document.UpdateDocument(mergedJsonBytes, request.ContentType);
            _context.ActivityStates.Update(state);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ActivityStateDocument>(state);
        }
    }
}
