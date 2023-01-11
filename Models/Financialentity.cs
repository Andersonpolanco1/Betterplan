using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Financialentity
    {
        public Financialentity()
        {
            Fundings = new HashSet<Funding>();
            Goals = new HashSet<Goal>();
            Goaltransactions = new HashSet<Goaltransaction>();
            Investmentstrategies = new HashSet<Investmentstrategy>();
            Investmentstrategytypes = new HashSet<Investmentstrategytype>();
            Portfolios = new HashSet<Portfolio>();
        }

        public string? Logo { get; set; }
        public string Title { get; set; } = null!;
        public string? Uuid { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string? Description { get; set; }
        public int? Defaultcurrencyid { get; set; }

        public virtual Currency? Defaultcurrency { get; set; }
        public virtual ICollection<Funding> Fundings { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Goaltransaction> Goaltransactions { get; set; }
        public virtual ICollection<Investmentstrategy> Investmentstrategies { get; set; }
        public virtual ICollection<Investmentstrategytype> Investmentstrategytypes { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
    }
}
