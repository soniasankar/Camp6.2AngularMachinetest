using LMS_Api.Models;
namespace LMS_Api.Repository
	{
		public interface ICustomerRepository
		{
			Task<Loan> ApplyForLoanAsync(Loan loan);

			Task<IEnumerable<Loan>> GetLoanRequestsByUserIdAsync(int userId);

			
			Task<IEnumerable<Help>> GetHelpSectionAsync();

			Task<Feedback> AddFeedbackAsync(Feedback feedback);
		}
}
