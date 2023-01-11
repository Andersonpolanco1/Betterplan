using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Goaltransactionfunding
    {
        public double Percentage { get; set; }
        public double? Amount { get; set; }
        public double? Quotas { get; set; }
        public DateOnly Date { get; set; }
        public int Id { get; set; }
        public int Fundingid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int? Transactionid { get; set; }
        public string State { get; set; } = null!;
        public string? Type { get; set; }
        public int Goalid { get; set; }
        public int Ownerid { get; set; }
        public double Cost { get; set; }

        public virtual Funding Funding { get; set; } = null!;
        public virtual Goal Goal { get; set; } = null!;
        public virtual User Owner { get; set; } = null!;
        public virtual Goaltransaction? Transaction { get; set; }
    }
}
