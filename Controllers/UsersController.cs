using AutoMapper;
using BetterplanAPI.Data.Abstract;
using BetterplanAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterplanAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet("{id}/summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSummaryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserSummaryAsync(int id)
        {
            if (!_usersRepository.UserExists(id))
                return NotFound();

            return Ok(await _usersRepository.GetUserSummaryByUserIdAsync(id));
        }

        [HttpGet("{id}/goals")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserGoalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoalsByUserIdAsync(int id)
        {
            if (!_usersRepository.UserExists(id))
                return NotFound();

            var userGoals = await _usersRepository.GetUserGoalsByUserIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<UserGoalDto>>(userGoals));
        }


        [HttpGet("{id}/goals/{goalId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserGoalDetailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoalDetailsByUserIdAsync(int id, int goalId)
        {
            var userGoal = await _usersRepository.GetUserGoalByGoalIdAsync(goalId);

            if(userGoal is null || userGoal.Userid != id)
                return NotFound();

            var goalDetailDto = _mapper.Map<UserGoalDetailDto>(userGoal);

            var goalBalance = await _usersRepository.GetBalanceByGoalIdOrOwnerId(userGoal.Id, true);
            goalDetailDto.GoalPercentage = (goalBalance / goalDetailDto.Targetamount) * 100;

            var transactions = await _usersRepository.GetTransactionsByGoalIdAsync(goalId);
            goalDetailDto.TotalContributions = transactions.Where(x => x.Type == "buy").Select(x => x.Amount).Sum();
            goalDetailDto.Totalwithdrawals = transactions.Where(x => x.Type == "sale").Select(x => x.Amount).Sum();

            return Ok(goalDetailDto);
        }
    }
}