/*using LMS_Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginsController : ControllerBase
	{

		private readonly ILoginRepository _loginRepository;

		//Get Configuration from appsettings - SecretKey
		private IConfiguration _config;

		//Repository DI


		//DI
		public LoginsController(IConfiguration config, ILoginRepository loginRepository)
		{
			_config = config;
			_loginRepository = loginRepository;
		}

		#region Valid Credentials-Username and Password
		// GET: api/Logins/username/password
		//Select the username and passwrod from the TblUser
		[HttpGet("{username}/{password}")] //?querystring
		public async Task<IActionResult> Users(string username, string password)
		{ // Variables for tracking unauthorized
			IActionResult response = Unauthorized(); // 401
			Login login = null;

			// 1. Authenticate the user by passing username and password
			login = await _loginRepository.ValidateUser(username, password);

			// 2. Generate JWT Token
			if (login != null)
			{
				// Custom Method for generating token
				var tokenString = GenerateJWTToken(login);

				response = Ok(new
				{
					uName = login.Username,
					roleId = login.RoleId,
					loginId = login.LoginId,
					token = tokenString
				});
			}
			return response;
		}
		#endregion

		#region GenerateJWTToken - Custom
		private object GenerateJWTToken(Login login)
		{
			// 1. Secret Security Key
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

			// 2. Pass Algorithm-Header
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			// 3. JWT Token - Payload
			var token = new JwtSecurityToken(
				_config["Jwt:Issuer"],
				_config["Jwt:Issuer"],
				null,
				expires: DateTime.Now.AddMinutes(20),
				signingCredentials: credentials
			);

			// 4. Writing Token
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		#endregion
	}
}
*/
using System.Threading.Tasks;
using LMS_Api.Models;
using LMS_Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LMS_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginsController : ControllerBase
	{
		private readonly ILoginRepository _loginRepository;
		private readonly IConfiguration _config;

		public LoginsController(IConfiguration config, ILoginRepository loginRepository)
		{
			_config = config;
			_loginRepository = loginRepository;
		}

		[HttpGet("{username}/{password}")]
		public async Task<IActionResult> Users(string username, string password)
		{
			IActionResult response = Unauthorized();
			User user = await _loginRepository.ValidateUser(username, password);

			if (user != null)
			{
				var tokenString = GenerateJWTToken(user);
				response = Ok(new
				{
					uName = user.Username,
					roleId = user.RoleId,
					userId = user.UserId,
					token = tokenString
				});
			}
			return response;
		}

		private object GenerateJWTToken(User user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				_config["Jwt:Issuer"],
				_config["Jwt:Issuer"],
				null,
				expires: DateTime.Now.AddMinutes(20),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
