using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Portfoliocomposition
    {
        public double? Percentage { get; set; }
        public int Id { get; set; }
        public int Subcategoryid { get; set; }
        public int Portfolioid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual Portfolio Portfolio { get; set; } = null!;
        public virtual Compositionsubcategory Subcategory { get; set; } = null!;
    }
}
