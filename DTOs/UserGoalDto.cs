namespace BetterplanAPI.DTOs
{
    public class UserGoalDto : BaseGoalDto
    {
        public DateTime Created { get; set; }
        public PortfolioDto? Portfolio { get; set; }
    }
}
