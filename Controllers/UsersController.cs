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
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [Route("{id}/summary")]
        [HttpGet]
        public async Task<IActionResult> GetUserSummaryAsync(int id)
        {
            var summary = await _usersRepository.GetUserSummaryByUserIdAsync(id);
            return summary is null ? NotFound() : Ok(summary);
        }

        [Route("{id}/goals")]
        [HttpGet]
        public async Task<IActionResult> GetGoalsByUserIdAsync(int id)
        {
            return Ok(await _usersRepository.GetUserGoalsByUserIdAsync(id));
        }


        [Route("{id}/goals/{goalId}")]
        [HttpGet]
        public async Task<IActionResult> GetGoalDetailsByUserIdAsync(int id, int goalId)
        {
            var goalDetails = await _usersRepository.GetUserGoalDetailsByUserIdAsync(id, goalId);
            return goalDetails is null ? NotFound() : Ok(goalDetails);

        }
    }
}
