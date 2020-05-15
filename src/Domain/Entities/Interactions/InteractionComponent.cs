using System;
using System.Diagnostics.CodeAnalysis;
using Doctrina.Domain.Entities.OwnedTypes;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class InteractionComponent: IEquatable<InteractionComponent>
    {
        public string Id { get; set; }

        public LanguageMapCollection Description { get; set; }

        public override bool Equals(object obj) =>
            (obj is InteractionComponent)
                ? Equals(obj)
                : false;

        public bool Equals([AllowNull] InteractionComponent other) =>
            other is null
                ? false
                : Id.Equals(other.Id);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
