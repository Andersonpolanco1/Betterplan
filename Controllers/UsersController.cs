using BetterplanAPI.Data.Abstract;
using BetterplanAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpGet("{id}/summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSummaryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserSummaryAsync(int id)
        {
            var summary = await _usersRepository.GetUserSummaryByUserIdAsync(id);
            return summary is null ? NotFound() : Ok(summary);
        }

        [HttpGet("{id}/goals")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserGoalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoalsByUserIdAsync(int id)
        {
            var goals = await _usersRepository.GetUserGoalsByUserIdAsync(id);
            return goals.Any() ? Ok(goals) : NotFound();
        }


        [HttpGet("{id}/goals/{goalId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserGoalDetailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoalDetailsByUserIdAsync(int id, int goalId)
        {
            var goalDetails = await _usersRepository.GetUserGoalDetailsByUserIdAsync(id, goalId);
            return goalDetails is null ? NotFound() : Ok(goalDetails);

        }
    }
}