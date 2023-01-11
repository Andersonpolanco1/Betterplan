using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Investmentstrategy
    {
        public Investmentstrategy()
        {
            Portfolios = new HashSet<Portfolio>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string[] Features { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Financialentityid { get; set; }
        public bool Isvisible { get; set; }
        public bool Isdefault { get; set; }
        public string Shorttitle { get; set; } = null!;
        public int? Investmentstrategytypeid { get; set; }
        public string? Iconurl { get; set; }
        public bool Isrecommended { get; set; }
        public string? Recommendedfor { get; set; }
        public int Displayorder { get; set; }

        public virtual Financialentity Financialentity { get; set; } = null!;
        public virtual Investmentstrategytype? Investmentstrategytype { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
    }
}
