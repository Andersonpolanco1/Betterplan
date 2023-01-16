using AutoMapper;
using BetterplanAPI.Data;
using BetterplanAPI.Data.Abstract;
using BetterplanAPI.DTOs;
using BetterplanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterplanAPI.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.Advisor)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<UserSummaryDto> GetUserSummaryByUserIdAsync(int userId)
        {
            double balance = await GetBalanceByGoalIdOrOwnerId(userId,false);

            double currentContributions = _context.Goaltransactions
                .Where(g => g.Ownerid == userId)
                .Select(x => x.Amount ?? 0d)
                .ToList()
                .Sum();

            return new UserSummaryDto { Balance = balance, CurrentContributions = currentContributions };
        }

        public async Task<IEnumerable<Goal?>> GetUserGoalsByUserIdAsync(int userId)
        {
            return  await _context.Goals.AsNoTracking()
                .Include(g => g.Portfolio)
                .Include(g => g.Financialentity)
                .Where(g => g.Userid == userId)
                .ToListAsync();
        }

        public async Task<Goal?> GetUserGoalByGoalIdAsync(int goalId)
        {
            return await  _context.Goals.AsNoTracking()
                .Include(g => g.Goalcategory)
                .Include(g => g.Financialentity)
                .Where(g => g.Id == goalId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Goaltransaction>>GetTransactionsByGoalIdAsync(int goalId)
        {
            return await _context.Goaltransactions.AsNoTracking().Where(g => g.Goalid == goalId).ToListAsync();
        }

        public async Task<double> GetBalanceByGoalIdOrOwnerId(int parameterId, bool isGoal)
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

        public bool UserExists(int userId) =>  _context.Users.Any(u => u.Id == userId);
    }
}