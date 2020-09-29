using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.Domain.Models.ValueObjects;
using System;

namespace Doctrina.Domain.Models
{
    public interface IActivityDefinitionEntity
    {
        LanguageMapCollection Names { get; set; }
        LanguageMapCollection Descriptions { get; set; }
        string Type { get; set; }
        string MoreInfo { get; set; }
        InteractionActivityBase InteractionActivity { get; set; }
        ExtensionsCollection Extensions { get; set; }
    }
}
