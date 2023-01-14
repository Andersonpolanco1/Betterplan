namespace BetterplanAPI.DTOs
{
    public abstract class BaseGoalDto
    {
        public string Title { get; set; } = string.Empty;
        public int Years { get; set; }
        public int Initialinvestment { get; set; }
        public int Monthlycontribution { get; set; }
        public int Targetamount { get; set; }
        public string Financialentity { get; set; } = string.Empty;
    }
}
