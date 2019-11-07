using Doctrina.Domain.Infrastructure;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class Account : ValueObject
    {
        public string HomePage { get; set; }

        public string Name { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return HomePage;
            yield return Name;
        }
    }
}
