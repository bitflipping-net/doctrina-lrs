using System;

namespace Doctrina.Application.Common.Interfaces
{
    /// <summary>
    /// This is the singleton for the application
    /// </summary>
    public interface IDoctrinaAppContext
    {
        DateTimeOffset? ConsistentThroughDate { get; set; }
    }
}