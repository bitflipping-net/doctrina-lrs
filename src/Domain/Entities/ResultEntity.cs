using System;
using System.Collections.Generic;
using Doctrina.Domain.Entities.OwnedTypes;

namespace Doctrina.Domain.Entities
{
    public class ResultEntity
    {
        public Guid ResultId { get; set; }

        public bool? Success { get; set; }

        public bool? Completion { get; set; }

        public string Response { get; set; }

        /// <summary>
        /// Duration ticks
        /// </summary>
        public long? DurationTicks { get; set; }

        /// <summary>
        /// Duration combination
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Json structure of extensions data
        /// </summary>
        public ExtensionsCollection Extensions { get; set; }
        public ScoreEntity Score { get; set; }

        //public Guid StatementId { get; set; }
        //public virtual StatementBaseEntity Statement { get; set; }
    }
}
