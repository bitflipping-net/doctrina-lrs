using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Common.Exceptions;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Data.Json;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class UpdateStateDocumentHandler : IRequestHandler<UpdateStateDocumentCommand, ActivityStateDocument>
    {
        private readonly IStoreDbContext _storeContext;
        private readonly IMapper _mapper;

        public UpdateStateDocumentHandler(IStoreDbContext storeContext, IMapper mapper)
        {
            _storeContext = storeContext;
            _mapper = mapper;
        }

        public async Task<ActivityStateDocument> Handle(UpdateStateDocumentCommand request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            IQueryable<ActivityStateEntity> query = _storeContext.Documents
                .OfType<ActivityStateEntity>()
                .Where(x => x.StateId == request.StateId)
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Persona.PersonaId == request.Persona.PersonaId);

            if (request.Registration.HasValue && request.Registration.Value != Guid.Empty)
            {
                query.Where(x => x.RegistrationId == request.Registration);
            }

            ActivityStateEntity state = await query.SingleOrDefaultAsync(cancellationToken);

            if (state == null)
            {
                throw new NotFoundException("State", request.StateId);
            }

            var stateDocument = state;
            if (stateDocument.ContentType != MediaTypes.Application.Json
            || request.ContentType != MediaTypes.Application.Json)
            {
                throw new BadRequestException();
            }

            JsonString jsonString = Encoding.UTF8.GetString(request.Content);
            JsonString savedJsonString = Encoding.UTF8.GetString(stateDocument.Content);
            // Merge, and overwrite duplicate props with props in new json document
            savedJsonString.Merge(jsonString);

            byte[] mergedJsonBytes = Encoding.UTF8.GetBytes(jsonString.ToString());

            state.UpdateDocument(mergedJsonBytes, request.ContentType);

            _storeContext.Documents.Update(state);

            await _storeContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityStateDocument>(state);
        }
    }
}
