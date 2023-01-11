using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Investmentstrategytype
    {
        public Investmentstrategytype()
        {
            Investmentstrategies = new HashSet<Investmentstrategy>();
        }

        public int Id { get; set; }
        public string Iconurl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Shorttitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Recommendedfor { get; set; } = null!;
        public bool Isvisible { get; set; }
        public bool Isdefault { get; set; }
        public int Displayorder { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int? Financialentityid { get; set; }

        public virtual Financialentity? Financialentity { get; set; }
        public virtual ICollection<Investmentstrategy> Investmentstrategies { get; set; }
    }
}
