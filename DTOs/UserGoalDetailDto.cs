using BetterplanAPI.Models;

namespace BetterplanAPI.DTOs
{
    public class UserGoalDetailDto : BaseGoalDto
    {
        public string GoalcategoryName { get; set; } = string.Empty;
        public double? Totalwithdrawals { get; set; }
        public double? TotalContributions { get; set; }
        public double? GoalPercentage { get; set; }
    }
}
