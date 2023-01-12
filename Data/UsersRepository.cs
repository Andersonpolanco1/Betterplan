﻿using BetterplanAPI.Data.Abstract;
using BetterplanAPI.Data.DTO;
using BetterplanAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BetterplanAPI.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> GetUserById(int userId)
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.InverseAdvisor)
                .Select(u => new UserDto{Id = u.Id, UserFullName = u.FullName, AdvisorFullName = u.Advisor.FullName, Created = u.Created})
                .FirstOrDefaultAsync(u => u.Id == userId)
                ;
        }

        public async Task<UserSummaryDto?> GetSummaryByUserId(int id)
        {
            return await  _context.Goaltransactionfundings
                .Where(g => g.Ownerid == id)
                .GroupBy(g => g.Ownerid)
                .Select(u => new UserSummaryDto { Balance = u.Sum(s => s.Quotas.Value) })
                .FirstAsync();
                

        }

        public async Task<IEnumerable<UserGoalDto?>> GetGoalsByUserId(int userId)
        {
            return await _context.Goals.AsNoTracking()
                .Include(g => g.Portfolio)
                .Include(g => g.Financialentity)
                .Where(g => g.Userid == userId)
                .Select(g => new UserGoalDto
                {
                    Title= g.Title,
                    Years = g.Years,
                    Initialinvestment = g.Initialinvestment,
                    Monthlycontribution = g.Monthlycontribution,
                    Targetamount = g.Targetamount,
                    Financialentity = g.Financialentity.Title,
                    Created = g.Created,
                    Portfolio = g.Portfolio
                }).ToListAsync();
        }

        public UserGoalDetailDto? GetGoalDetailsByUserId(int userId, int goalId)
        {
            var goalDetail =  _context.Goals.AsNoTracking()
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
                }).FirstOrDefault();

            if (goalDetail is null)
                return null;

            var transactions = GetGoalTransactions(goalId);

            goalDetail.GoaltransactionsTotalBuyAmount = transactions.Where(x => x.Type == "buy").Select(x => x.Amount).Sum();
            goalDetail.GoaltransactionsTotalSaleAmount = transactions.Where(x => x.Type == "sale").Select(x => x.Amount).Sum();


            return goalDetail;

        }

        private IEnumerable<Goaltransaction> GetGoalTransactions(int goalId)
        {
            return _context.Goaltransactions.AsNoTracking().Where(g => g.Goalid == goalId).ToList();
        }
    }
}
