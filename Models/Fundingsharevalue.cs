using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Fundingsharevalue
    {
        public double Value { get; set; }
        public DateOnly Date { get; set; }
        public int Id { get; set; }
        public int Fundingid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual Funding Funding { get; set; } = null!;
    }
}
