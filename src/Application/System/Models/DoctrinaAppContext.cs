using Doctrina.Application.Common.Interfaces;
using System;

namespace Doctrina.Application.Infrastructure
{
    public class DoctrinaAppContext : IDoctrinaAppContext
    {
        public DateTimeOffset? ConsistentThroughDate { get; set; }
    }
}
