using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Domain.Entities.OwnedTypes;
using System;

namespace Doctrina.Domain.Entities
{
    /// <summary>
    /// Definition for an activity
    /// </summary>
    public interface IActivityDefinitionEntity
    {
        Guid ActivityDefinitionId { get; set; }
        LanguageMapCollection Names { get; set; }
        LanguageMapCollection Descriptions { get; set; }
        string Type { get; set; }
        string MoreInfo { get; set; }
        InteractionActivityBase InteractionActivity { get; set; }
        ExtensionsCollection Extensions { get; set; }
    }
}
