using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Queries;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Doctrina.Application.Infrastructure
{
    public class DoctrinaAppContext : IDoctrinaAppContext
    {
        private readonly IMediator _mediator;

        public DoctrinaAppContext(IMediator mediator)
        {
            _mediator = mediator;
        }

        private DateTimeOffset? _consistentThroughDate;
        public DateTimeOffset ConsistentThroughDate {
            get { 
                if (!_consistentThroughDate.HasValue) 
                {
                    // Jesus this is just sad
                    _consistentThroughDate = Task.Run(async () => await _mediator.Send(new ConsistentThroughQuery())).Result;
                }
                return _consistentThroughDate.Value;
            }
            set { _consistentThroughDate = value; }
        }
    }
}
