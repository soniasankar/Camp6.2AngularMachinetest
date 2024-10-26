using LMS_Api.Models;

namespace LMS_Api.Repository
{
	public interface ILoginRepository
	{
		Task<User> ValidateUser(string username, string password);
	}
}
