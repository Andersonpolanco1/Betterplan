using System;
using System.Collections.Generic;

namespace BetterplanAPI.Models
{
    public partial class Currency
    {
        public Currency()
        {
            CurrencyindicatorDestinationcurrencies = new HashSet<Currencyindicator>();
            CurrencyindicatorSourcecurrencies = new HashSet<Currencyindicator>();
            Financialentities = new HashSet<Financialentity>();
            Fundings = new HashSet<Funding>();
            GoalCurrencies = new HashSet<Goal>();
            GoalDisplaycurrencies = new HashSet<Goal>();
            Goaltransactions = new HashSet<Goaltransaction>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Uuid { get; set; } = null!;
        public string? Yahoomnemonic { get; set; }
        public string? Currencycode { get; set; }
        public string? Digitsinfo { get; set; }
        public string? Display { get; set; }
        public string? Locale { get; set; }
        public string? Serverformatnumber { get; set; }

        public virtual ICollection<Currencyindicator> CurrencyindicatorDestinationcurrencies { get; set; }
        public virtual ICollection<Currencyindicator> CurrencyindicatorSourcecurrencies { get; set; }
        public virtual ICollection<Financialentity> Financialentities { get; set; }
        public virtual ICollection<Funding> Fundings { get; set; }
        public virtual ICollection<Goal> GoalCurrencies { get; set; }
        public virtual ICollection<Goal> GoalDisplaycurrencies { get; set; }
        public virtual ICollection<Goaltransaction> Goaltransactions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
