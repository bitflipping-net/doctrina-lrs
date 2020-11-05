using System;

namespace Doctrina.Domain.Models
{
    public class Scope
    {
        /// <summary>
        /// Unique key for the scope
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Unique name for the scope
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the scope
        /// </summary>
        public string Description { get; set; }
    }
}
