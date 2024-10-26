using LMS_Api.Models;
using LMS_Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerController(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		// Apply for a loan
		[HttpPost("apply")]
		public async Task<IActionResult> ApplyForLoan([FromBody] Loan loan)
		{
			if (loan == null)
			{
				return BadRequest("Loan data is required.");
			}

			try
			{
				var createdLoan = await _customerRepository.ApplyForLoanAsync(loan);
				return CreatedAtAction(nameof(GetLoanRequests), new { userId = createdLoan.UserId }, createdLoan);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message); // Internal Server Error
			}
		}

		// Get loan requests by user ID
		[HttpGet("loans/{userId}")]
		public async Task<IActionResult> GetLoanRequests(int userId)
		{
			try
			{
				var loans = await _customerRepository.GetLoanRequestsByUserIdAsync(userId);
				return Ok(loans);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message); // Internal Server Error
			}
		}

		// Get help section
		[HttpGet("help")]
		public async Task<IActionResult> GetHelpSection()
		{
			try
			{
				var helpInfo = await _customerRepository.GetHelpSectionAsync();
				return Ok(helpInfo);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message); // Internal Server Error
			}
		}

		// Add feedback
		[HttpPost("feedback")]
		public async Task<IActionResult> AddFeedback([FromBody] Feedback feedback)
		{
			if (feedback == null)
			{
				return BadRequest("Feedback data is required.");
			}

			try
			{
				var createdFeedback = await _customerRepository.AddFeedbackAsync(feedback);
				return CreatedAtAction(nameof(AddFeedback), createdFeedback);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message); // Internal Server Error
			}
		}
	}
}
