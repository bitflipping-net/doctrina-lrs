using Doctrina.Domain.Models.ValueObjects;
using System;

namespace Doctrina.Domain.Models
{
    public class ResultModel
    {
        public int ResultId { get; set; }

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
        /// JSON encoded string of Extensions Object
        /// </summary>
        public ExtensionsCollection Extensions { get; set; }

        /// <summary>
        /// The score of the result
        /// </summary>
        public Score Score { get; set; }

        public int StatementId { get; set; }

        public StatementBaseModel Statement { get; set; }
    }
}
