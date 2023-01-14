using BetterplanAPI.Models;

namespace BetterplanAPI.DTOs
{
    public class UserGoalDto : BaseGoalDto
    {
        public DateTime Created { get; set; }
        public Portfolio? Portfolio { get; set; }
    }
}
