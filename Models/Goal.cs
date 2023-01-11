using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Goal
    {
        public Goal()
        {
            Goaltransactionfundings = new HashSet<Goaltransactionfunding>();
            Goaltransactions = new HashSet<Goaltransaction>();
        }

        public string Title { get; set; } = null!;
        public int Years { get; set; }
        public int Initialinvestment { get; set; }
        public int Monthlycontribution { get; set; }
        public int Targetamount { get; set; }
        public int Id { get; set; }
        public int Userid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Goalcategoryid { get; set; }
        public int? Risklevelid { get; set; }
        public int? Portfolioid { get; set; }
        public int? Financialentityid { get; set; }
        public int? Currencyid { get; set; }
        public int? Displaycurrencyid { get; set; }

        public virtual Currency? Currency { get; set; }
        public virtual Currency? Displaycurrency { get; set; }
        public virtual Financialentity? Financialentity { get; set; }
        public virtual Goalcategory Goalcategory { get; set; } = null!;
        public virtual Portfolio? Portfolio { get; set; }
        public virtual Risklevel? Risklevel { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Goaltransactionfunding> Goaltransactionfundings { get; set; }
        public virtual ICollection<Goaltransaction> Goaltransactions { get; set; }
    }
}
