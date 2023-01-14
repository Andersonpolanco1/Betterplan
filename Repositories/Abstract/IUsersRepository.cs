using BetterplanAPI.DTOs;
using BetterplanAPI.Models;

namespace BetterplanAPI.Data.Abstract
{
    public interface IUsersRepository
    {
        Task<UserDto?> GetUserByIdAsync(int userId);

        Task<UserSummaryDto?> GetUserSummaryByUserIdAsync(int userId);

        Task<IEnumerable<UserGoalDto?>> GetUserGoalsByUserIdAsync(int userId);

        Task<UserGoalDetailDto?> GetUserGoalDetailsByUserIdAsync(int userId, int goalId);
    }
}
