using System;
using Doctrina.Domain.Models.ValueObjects;

namespace Doctrina.Domain.Models.Interfaces
{
    public interface IVerbModel
    {
        Guid VerbId { get; set; }
        string Hash { get; set; }
        string Id { get; set; }
        LanguageMapCollection Display { get; set; }
    }
}
