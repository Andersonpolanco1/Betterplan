using BetterplanAPI.Data;
using BetterplanAPI.Data.Abstract;
using BetterplanAPI.DTOs;
using BetterplanAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            double balance = await GetBalanceByUserIdAsync(userId);

            double CurrentContributions = _context.Goaltransactions
                .Where(g => g.Ownerid == userId)
                .Select(x => x.Amount ?? 0d)
                .ToList()
                .Sum();

            return new UserSummaryDto { Balance = balance, CurrentContributions = CurrentContributions };
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

            var transactions = await _context.Goaltransactions.AsNoTracking().Where(g => g.Goalid == goalId).ToListAsync();

            goalDetail.GoaltransactionsTotalBuyAmount = transactions.Where(x => x.Type == "buy").Select(x => x.Amount).Sum();
            goalDetail.GoaltransactionsTotalSaleAmount = transactions.Where(x => x.Type == "sale").Select(x => x.Amount).Sum();


            return goalDetail;

        }


        private async Task<double> GetBalanceByUserIdAsync(int userId)
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
                where goalTransaction.Ownerid == userId
                select new TransactionBalance { Goaltransactionfunding = goalTransaction, Currencyindicator = ci, Fundingsharevalue = fundingshare }
                .GetBalance();

            var balances = await totalTransactionsBalance.ToListAsync();

            return balances.Sum();
        }
    }
}