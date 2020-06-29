using MediatR;
using System;

namespace Doctrina.Application.Statements.Queries
{
    public class ConsistentThroughQuery : IRequest<DateTimeOffset>
    {
    }
}
