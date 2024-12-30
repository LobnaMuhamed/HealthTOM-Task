using Web_API.Models;
using Web_API.Models.DTOs.Requests;
using Web_API.Models.DTOs.Responses;

namespace Web_API.Interfaces
{
	public interface IAccountService
	{
		Task<AuthResponse> RegisterAsync(RegisterRequest registerDto);
		Task<AuthResponse> LoginAsync(LoginRequest loginDto);
		Task<UserResponse> GetUserDetailsAsync(int userId);
		string GenerateToken(User user);
	}
}
