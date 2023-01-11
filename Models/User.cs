using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class User
    {
        public User()
        {
            Goals = new HashSet<Goal>();
            Goaltransactionfundings = new HashSet<Goaltransactionfunding>();
            Goaltransactions = new HashSet<Goaltransaction>();
            InverseAdvisor = new HashSet<User>();
        }

        public string Firstname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int Id { get; set; }
        public int? Advisorid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int? Currencyid { get; set; }

        public virtual User? Advisor { get; set; }
        public virtual Currency? Currency { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Goaltransactionfunding> Goaltransactionfundings { get; set; }
        public virtual ICollection<Goaltransaction> Goaltransactions { get; set; }
        public virtual ICollection<User> InverseAdvisor { get; set; }
    }
}
