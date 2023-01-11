using BetterplanAPI.Data.Abstract;
using BetterplanAPI.Data.DTO;
using BetterplanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterplanAPI.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> GetById(int id)
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.InverseAdvisor)
                .Select(u => new UserDto{Id = u.Id, UserFullName = u.FullName, AdvisorFullName = u.Advisor.FullName, Created = u.Created})
                .FirstOrDefaultAsync(u => u.Id == id)
                ;
        }


    }
}
