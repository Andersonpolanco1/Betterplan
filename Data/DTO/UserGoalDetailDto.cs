using BetterplanAPI.Models;

namespace BetterplanAPI.Data.DTO
{
    public class UserGoalDetailDto : BaseGoalDto
    {
        public string GoalcategoryName { get; set; } = string.Empty;
        public double? GoaltransactionsTotalSaleAmount { get; set; }
        public double? GoaltransactionsTotalBuyAmount { get; set; }
        public double? GoalPercentage { get; set; }
    }
}
