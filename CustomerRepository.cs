using LMS_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS_Api.Repository
	{
		public class CustomerRepository : ICustomerRepository
		{
			private readonly LmsContext _context;

			public CustomerRepository(LmsContext context)
			{
				_context = context;
			}

			public async Task<Loan> ApplyForLoanAsync(Loan loan)
			{
				try
				{
					await _context.Loans.AddAsync(loan);
					await _context.SaveChangesAsync();
					return loan;
				}
				catch (DbUpdateException ex)
				{
					// Handle database update exceptions (e.g., unique constraint violations)
					// Log error here (omitted for brevity)
					throw new Exception("Could not apply for loan. Please try again.", ex);
				}
				catch (Exception ex)
				{
					// Handle other exceptions
					// Log error here (omitted for brevity)
					throw new Exception("An error occurred while applying for the loan.", ex);
				}
			}

			public async Task<IEnumerable<Loan>> GetLoanRequestsByUserIdAsync(int userId)
			{
				try
				{
					return await _context.Loans
						.Where(loan => loan.UserId == userId)
						.ToListAsync();
				}
				catch (Exception ex)
				{
					// Log error here (omitted for brevity)
					throw new Exception("An error occurred while retrieving loan requests.", ex);
				}
			}

			public async Task<IEnumerable<Help>> GetHelpSectionAsync()
			{
				try
				{
					return await _context.Helps.ToListAsync();
				}
				catch (Exception ex)
				{
					// Log error here (omitted for brevity)
					throw new Exception("An error occurred while retrieving help information.", ex);
				}
			}

			public async Task<Feedback> AddFeedbackAsync(Feedback feedback)
			{
				try
				{
					await _context.Feedbacks.AddAsync(feedback);
					await _context.SaveChangesAsync();
					return feedback;
				}
				catch (DbUpdateException ex)
				{
					// Handle database update exceptions
					// Log error here (omitted for brevity)
					throw new Exception("Could not submit feedback. Please try again.", ex);
				}
				catch (Exception ex)
				{
					// Handle other exceptions
					// Log error here (omitted for brevity)
					throw new Exception("An error occurred while submitting feedback.", ex);
				}
			}
		}
}



