using Microsoft.AspNetCore.Mvc;
using LMS_Api.Models;
using LMS_Api.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpGet("{id}", Name = "GetUserById")]
		public async Task<ActionResult<User>> GetUserByIdAsync(int id)
		{
			var user = await _userRepository.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpGet("all", Name = "GetAllUsers")]
		public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllUsersAsync();
			return Ok(users);
		}

		[HttpPost("register", Name = "RegisterUser")]
		public async Task<ActionResult<User>> RegisterUserAsync([FromBody] User user)
		{
			if (user == null)
			{
				return BadRequest("User data is null.");
			}

			try
			{
				var createdUser = await _userRepository.RegisterUserAsync(user);
				return CreatedAtRoute("GetUserById", new { id = createdUser.UserId }, createdUser);
			}
			catch (Exception ex)
			{
				// Log exception (ex)
				return StatusCode(500, "Internal server error while registering user.");
			}
		}

		[HttpPut("{id}", Name = "UpdateUser")]
		public async Task<ActionResult> UpdateUserAsync(int id, [FromBody] User user)
		{
			if (user == null || user.UserId != id)
			{
				return BadRequest("User data is invalid.");
			}

			try
			{
				await _userRepository.UpdateUserAsync(user);
				return NoContent();
			}
			catch (Exception ex)
			{
				// Log exception (ex)
				return StatusCode(500, "Internal server error while updating user.");
			}
		}

		[HttpDelete("{id}", Name = "DeleteUser")]
		public async Task<ActionResult> DeleteUserAsync(int id)
		{
			try
			{
				await _userRepository.DeleteUserAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				// Log exception (ex)
				return StatusCode(500, "Internal server error while deleting user.");
			}
		}
	}
}
