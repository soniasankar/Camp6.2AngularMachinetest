using LMS_Api.Models;

namespace LMS_Api.Repository
{
	public interface IUserRepository
	{
		Task<User> GetUserByIdAsync(int userId);
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User> RegisterUserAsync(User user);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int userId);
	}
}
