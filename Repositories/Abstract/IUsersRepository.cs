using BetterplanAPI.DTOs;
using BetterplanAPI.Models;

namespace BetterplanAPI.Data.Abstract
{
    public interface IUsersRepository
    {
        Task<User?> GetUserByIdAsync(int userId);

        Task<UserSummaryDto> GetUserSummaryByUserIdAsync(int userId);

        Task<IEnumerable<Goal?>> GetUserGoalsByUserIdAsync(int userId);

        Task<Goal?> GetUserGoalByGoalIdAsync(int goalId);

        Task<IEnumerable<Goaltransaction>> GetTransactionsByGoalIdAsync(int goalId);

        bool UserExists(int userId);


        /// <summary>
        /// Gets the balance by the Id of a user or by the Id of a goal
        /// </summary>
        /// <param name="parameterId">UserId or GoalId of GoalTransaction entity. </param>
        /// <param name="isGoal">true for get balance by goalId, false for get balance by ownerId.</param>
        /// <returns></returns>
        Task<double> GetBalanceByGoalIdOrOwnerId(int parameterId, bool isGoal);
    }
}
