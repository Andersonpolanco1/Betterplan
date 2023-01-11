using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Compositionsubcategory
    {
        public Compositionsubcategory()
        {
            Fundingcompositions = new HashSet<Fundingcomposition>();
            Portfoliocompositions = new HashSet<Portfoliocomposition>();
        }

        public string Name { get; set; } = null!;
        public string? Uuid { get; set; }
        public string? Description { get; set; }
        public int Id { get; set; }
        public int? Categoryid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual Compositioncategory? Category { get; set; }
        public virtual ICollection<Fundingcomposition> Fundingcompositions { get; set; }
        public virtual ICollection<Portfoliocomposition> Portfoliocompositions { get; set; }
    }
}
