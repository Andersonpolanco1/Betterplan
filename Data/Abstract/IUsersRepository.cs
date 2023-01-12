using BetterplanAPI.Data.DTO;
using BetterplanAPI.Models;

namespace BetterplanAPI.Data.Abstract
{
    public interface IUsersRepository
    {
        Task<UserDto?> GetUserById(int id);

        Task<UserSummaryDto?> GetSummaryByUserId(int id);

        Task<IEnumerable<UserGoalDto?>> GetGoalsByUserId(int userId);
    }
}
