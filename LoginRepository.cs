using LMS_Api.Models;
using Microsoft.EntityFrameworkCore;
namespace LMS_Api.Repository
{
	public class LoginRepository:ILoginRepository
	{

		private readonly LmsContext _context;

		public LoginRepository(LmsContext context)
		{
			_context = context;
		}
		public async Task<User> ValidateUser(string username, string password)
		{
			try
			{
				if (_context != null)
				{
					var UserL = await _context.Users
						.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

					return UserL; // Return the user or null if not found
				}

				return null; // Context is null
			}
			catch (Exception ex)
			{
				// Log the exception (consider using a logging framework)
				// You can rethrow or handle it as needed
				throw new Exception($"An error occurred while validating the user: {ex.Message}", ex);
			}
		}
	}
}

