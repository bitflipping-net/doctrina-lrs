using Doctrina.Domain.Entities.OwnedTypes;
using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IVerbEntity
    {
        Guid VerbId { get; set; }
        string Hash { get; set; }
        string Id { get; set; }
        LanguageMapCollection Display { get; set; }
    }
}
