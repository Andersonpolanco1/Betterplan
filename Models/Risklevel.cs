using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Risklevel
    {
        public Risklevel()
        {
            Goals = new HashSet<Goal>();
            Portfolios = new HashSet<Portfolio>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
    }
}
