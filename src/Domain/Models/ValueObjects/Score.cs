using Doctrina.Domain.Infrastructure;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// JSON Encoded string of the xAPI Score Object
    /// </summary>
    public class Score
    {
        public double? Scaled { get; set; }

        public double? Raw { get; set; }

        public double? Min { get; set; }

        public double? Max { get; set; }
    }
}
