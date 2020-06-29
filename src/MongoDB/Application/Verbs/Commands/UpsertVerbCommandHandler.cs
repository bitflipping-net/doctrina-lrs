using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Infrastructure;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.MongoDB.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Verbs.Commands
{
    public class UpsertVerbCommandHandler : IRequestHandler<UpsertVerbCommand, VerbEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpsertVerbCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<VerbEntity> Handle(UpsertVerbCommand request, CancellationToken cancellationToken)
        {
            string hash = request.Verb.Id.ComputeHash();
            VerbEntity verb = await _context.Verbs.Find(x => x.Hash == hash, null).FirstAsync(cancellationToken);

            if(verb == null)
            {
                verb = _mapper.Map<VerbEntity>(request.Verb);
                verb.VerbId = Guid.NewGuid();
                await _context.Verbs.InsertOneAsync(verb);
                return verb;
            }

            if (request.Verb.Display != null)
            {
                var filter = new BsonDocument();
                var update = new BsonDocument("$set", new BsonDocument("display", new BsonDocument()));
                var options = new UpdateOptions { IsUpsert = true };
                _context.Verbs.UpdateOneAsync(filter, update, options);
            }

            return verb;
        }
    }
}
