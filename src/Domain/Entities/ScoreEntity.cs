using System;

namespace Doctrina.Domain.Entities
{
    public class ScoreEntity
    {
        public double? Scaled { get; set; }

        public double? Raw { get; set; }

        public double? Min { get; set; }

        public double? Max { get; set; }
    }
}
