using LMS_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS_Api.Repository
{
	public interface IAdminRepository
	{
		// Customer Management
		Task<IEnumerable<User>> GetAllCustomersAsync();
		Task<User> ApproveCustomerAsync(int customerId);
		Task<User> RejectCustomerAsync(int customerId);

		// Loan Officer Management
		Task<IEnumerable<User>> GetAllLoanOfficersAsync();
		Task<User> ApproveLoanOfficerAsync(int officerId);
		Task<User> RejectLoanOfficerAsync(int officerId);

		// Background Verification Management
		Task AssignLoanOfficerForBackgroundVerificationAsync(int loanId, int officerId);
		Task<IEnumerable<BackgroundVerification>> GetAllBackgroundVerificationsAsync();
		Task<BackgroundVerification> GetBackgroundVerificationByIdAsync(int verificationId);
		Task UpdateBackgroundVerificationAsync(BackgroundVerification verification);
		Task DeleteBackgroundVerificationAsync(int verificationId);

		// Loan Verification Management
		Task AssignLoanOfficerForLoanVerificationAsync(int loanId, int officerId);
		Task<IEnumerable<LoanVerification>> GetAllLoanVerificationsAsync();
		Task<LoanVerification> GetLoanVerificationByIdAsync(int verificationId);
		Task UpdateLoanVerificationAsync(LoanVerification verification);
		Task DeleteLoanVerificationAsync(int verificationId);

		// Help Report Management
		Task<IEnumerable<Help>> GetAllHelpReportsAsync();
		Task UpdateHelpReportAsync(Help help);

		// Feedback Management
		Task AddFeedbackQuestionAsync(Feedback feedback);
		Task<IEnumerable<Feedback>> GetAllFeedbackQuestionsAsync();
		Task UpdateFeedbackQuestionAsync(Feedback feedback);
		Task<IEnumerable<Feedback>> GetCustomerFeedbackAsync();
	}
}
