using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Portfoliofunding
    {
        public double Percentage { get; set; }
        public int Id { get; set; }
        public int Fundingid { get; set; }
        public int Portfolioid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual Funding Funding { get; set; } = null!;
        public virtual Portfolio Portfolio { get; set; } = null!;
    }
}
