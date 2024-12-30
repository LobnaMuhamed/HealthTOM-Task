using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API.Interfaces;
using Web_API.Models.DTOs.Requests;
using Web_API.Models.DTOs.Responses;

namespace Web_API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly IUserContext _userContext;
		public AccountController(IAccountService accountService, IUserContext userContext)
		{
			_accountService = accountService;
			_userContext = userContext;
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest registerDto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invalid registeration data.");
			var response = await _accountService.RegisterAsync(registerDto);

			if (!response.IsSuccess)
				return BadRequest(response);
			return Ok(response);
		}


		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest loginDto)
		{
			var response = await _accountService.LoginAsync(loginDto);
			if (!response.IsSuccess)
				return Unauthorized(response);
			return Ok(response);
		}

		[HttpGet("detail")]
		public async Task<ActionResult<UserResponse>> GetUserDetail()
		{
			var userId = _userContext.GetCurrentUserId();
			try
			{
				var user = await _accountService.GetUserDetailsAsync(userId);
				return Ok(user);
			}
			catch (Exception ex)
			{
				return NotFound(new AuthResponse
				{
					IsSuccess = false,
					Message = ex.Message
				});
			}
		}

	}
}
