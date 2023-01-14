using BetterplanAPI.Data;
using BetterplanAPI.Data.Abstract;
using BetterplanAPI.DTOs;
using BetterplanAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;

namespace BetterplanAPI.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.InverseAdvisor)
                .Select(u => new UserDto { Id = u.Id, UserFullName = u.FullName, AdvisorFullName = u.Advisor.FullName, Created = u.Created })
                .FirstOrDefaultAsync(u => u.Id == userId)
                ;
        }

        public async Task<UserSummaryDto?> GetUserSummaryByUserIdAsync(int userId)
        {
            double balance = await GetBalanceByGoalIdOrOwnerId(userId,false);

            double currentContributions = _context.Goaltransactions
                .Where(g => g.Ownerid == userId)
                .Select(x => x.Amount ?? 0d)
                .ToList()
                .Sum();

            return new UserSummaryDto { Balance = balance, CurrentContributions = currentContributions };
        }

        public async Task<IEnumerable<UserGoalDto?>> GetUserGoalsByUserIdAsync(int userId)
        {
            return await _context.Goals.AsNoTracking()
                .Include(g => g.Portfolio)
                .Include(g => g.Financialentity)
                .Where(g => g.Userid == userId)
                .Select(g => new UserGoalDto
                {
                    Title = g.Title,
                    Years = g.Years,
                    Initialinvestment = g.Initialinvestment,
                    Monthlycontribution = g.Monthlycontribution,
                    Targetamount = g.Targetamount,
                    Financialentity = g.Financialentity.Title,
                    Created = g.Created,
                    Portfolio = g.Portfolio
                }).ToListAsync();
        }

        public async Task<UserGoalDetailDto?> GetUserGoalDetailsByUserIdAsync(int userId, int goalId)
        {
            var goalDetail = await  _context.Goals.AsNoTracking()
                .Include(g => g.Goalcategory)
                .Include(g => g.Financialentity)
                .Where(g => g.Userid == userId && g.Id == goalId)
                .Select(g => new UserGoalDetailDto
                {
                    Title = g.Title,
                    Years = g.Years,
                    Initialinvestment = g.Initialinvestment,
                    Monthlycontribution = g.Monthlycontribution,
                    Targetamount = g.Targetamount,
                    Financialentity = g.Financialentity.Title,
                    GoalcategoryName = g.Goalcategory.Title
                }).FirstOrDefaultAsync();

            if (goalDetail is null)
                return null;

            var goalBalance = await GetBalanceByGoalIdOrOwnerId(goalId, true);
            goalDetail.GoalPercentage = (goalBalance / goalDetail.Targetamount) * 100;

            var transactions = await _context.Goaltransactions.AsNoTracking().Where(g => g.Goalid == goalId).ToListAsync();

            goalDetail.TotalContributions = transactions.Where(x => x.Type == "buy").Select(x => x.Amount).Sum();
            goalDetail.Totalwithdrawals = transactions.Where(x => x.Type == "sale").Select(x => x.Amount).Sum();


            return goalDetail;

        }
        /// <summary>
        /// Gets the balance by the Id of a user or by the Id of a goal
        /// </summary>
        /// <param name="parameterId">UserId or GoalId of GoalTransaction entity. </param>
        /// <param name="isGoal">true for get balance by goalId, false for get balance by ownerId.</param>
        /// <returns></returns>
        private async Task<double> GetBalanceByGoalIdOrOwnerId(int parameterId, bool isGoal)
        {
            var goalTransactions = _context.Goaltransactionfundings.AsQueryable();
            var fundingShareValues = _context.Fundingsharevalues.AsQueryable();
            var currencyIndicators = _context.Currencyindicators.AsQueryable();
            var fundings = _context.Fundings.AsQueryable();
            var users = _context.Users.AsQueryable();

            var totalTransactionsBalance =
                from goalTransaction in goalTransactions
                join funding in fundings
                    on goalTransaction.Fundingid equals funding.Id
                join fundingShareValue in fundingShareValues
                    on new { fu = funding.Id, da = goalTransaction.Date }
                    equals new { fu = fundingShareValue.Fundingid, da = fundingShareValue.Date }
                    into fs
                from fundingshare in fs.DefaultIfEmpty()
                join user in users
                    on goalTransaction.Ownerid equals user.Id
                join currencyIndicator in currencyIndicators
                    on new { sid = user.Currencyid.Value, did = funding.Currencyid.Value, d = goalTransaction.Date }
                    equals new { sid = currencyIndicator.Sourcecurrencyid, did = currencyIndicator.Destinationcurrencyid, d = currencyIndicator.Date }
                    into cis
                from ci in cis.DefaultIfEmpty()            
                where isGoal ? goalTransaction.Goalid == parameterId : goalTransaction.Ownerid == parameterId
                select new TransactionBalance { Goaltransactionfunding = goalTransaction, Currencyindicator = ci, Fundingsharevalue = fundingshare }
                .GetBalance();

            var balances = await totalTransactionsBalance.ToListAsync();

            return balances.Sum();
        }
    }
}