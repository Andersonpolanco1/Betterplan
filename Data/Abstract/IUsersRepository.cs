using BetterplanAPI.Data.DTO;
using BetterplanAPI.Models;

namespace BetterplanAPI.Data.Abstract
{
    public interface IUsersRepository
    {
        Task<UserDto?> GetById(int id);
    }
}
