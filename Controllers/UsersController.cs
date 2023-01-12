using BetterplanAPI.Data;
using BetterplanAPI.Data.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterplanAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _usersRepository.GetUserById(id);
            return user is null ? NotFound() : Ok(user);
        }

        [Route("{id}/summary")]
        [HttpGet]
        public async Task<IActionResult> GetUserSummary(int id)
        {
            var summary = await _usersRepository.GetSummaryByUserId(id);
            return summary is null ? NotFound() : Ok(summary);
        }

        [Route("{id}/goals")]
        [HttpGet]
        public async Task<IActionResult> GetGoalsByUserId(int id)
        {
            var goals = await _usersRepository.GetGoalsByUserId(id);

            return Ok(goals);
        }
    }
}
