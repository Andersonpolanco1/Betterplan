namespace BetterplanAPI.Models
{
    public class TransactionBalance
    {
        public Goaltransactionfunding? Goaltransactionfunding { get; set; }
        public Fundingsharevalue? Fundingsharevalue { get; set; }
        public Currencyindicator? Currencyindicator { get; set; }

        public double GetBalance()
        {
            double Quotas = Goaltransactionfunding?.Quotas is null ? 1d : Goaltransactionfunding.Quotas.Value;
            double fundingsharevalue = Fundingsharevalue is null ? 1d : Fundingsharevalue.Value;
            double currencyindicator = Currencyindicator is null ? 1d : Currencyindicator.Value;

            return Quotas * fundingsharevalue * currencyindicator;
        }
    }
}
