using BetterplanAPI.Models;

namespace BetterplanAPI.Data.DTO
{
    public class UserGoalDto : BaseGoalDto
    {
        public DateTime Created { get; set; }
        public Portfolio? Portfolio { get; set; }
    }
}
