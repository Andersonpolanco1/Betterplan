using BetterplanAPI.Models;

namespace BetterplanAPI.Data.DTO
{
    public class UserGoalDto
    {
        public string Title { get; set; } = string.Empty;
        public int Years { get; set; }
        public int Initialinvestment { get; set; }
        public int Monthlycontribution { get; set; }
        public int Targetamount { get; set; }
        public string Financialentity { get; set; }
        public DateTime Created { get; set; }
        public Portfolio? Portfolio { get; set; }
    }
}
