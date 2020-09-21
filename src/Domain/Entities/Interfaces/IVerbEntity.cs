using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.Domain.Entities.ValueObjects;
using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IVerbEntity
    {
        Guid VerbId { get; set; }
        string Hash { get; set; }
        string IRI { get; set; }
        LanguageMapCollection Display { get; set; }
    }
}
