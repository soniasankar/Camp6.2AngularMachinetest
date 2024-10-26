using LMS_Api.Models;

namespace LMS_Api.Repository
{
	public interface ILoanRepository
	{
		Task<Loan> GetLoanByIdAsync(int loanId);
		Task<IEnumerable<Loan>> GetAllLoansAsync();
		Task<Loan> CreateLoanAsync(Loan loan);
		Task UpdateLoanAsync(Loan loan);
		Task DeleteLoanAsync(int loanId);
	}
}
