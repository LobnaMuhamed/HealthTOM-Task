using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Web_API.Interfaces;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;
using Web_API.Models.DTOs.Responses;
using Web_API.Repositories;

namespace Web_API.Services
{
	public class AccountService : IAccountService
	{
		private readonly IMapper _mapper;
		private readonly UserRepository _userRepository;
		private readonly IConfiguration _configuration;
		private readonly IUserContext _userContext;

		public AccountService(IMapper mapper, IConfiguration configuration, IUserContext userContext, UserRepository userRepository)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_configuration = configuration;
			_userContext = userContext;
		}
		public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
		{

			var existingUser = await _userRepository.GetUserByEmailAsync(registerRequest.Email);
			if (existingUser != null)
				return new AuthResponse
				{
					IsSuccess = false,
					Message = "Email already exists."
				};
			var user = _mapper.Map<User>(registerRequest);
			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			user.UserId = await _userRepository.RegisterUserAsync(user);

			return new AuthResponse
			{
				IsSuccess = true,
				Message = "Registeration successful!"
			};
		}





		public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
		{
			var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
				return new AuthResponse
				{
					IsSuccess = false,
					Message = "Invalid email or password!"
				};
			var token = GenerateToken(user);
			return new AuthResponse
			{
				IsSuccess = true,
				Message = "Login Successful!",
				Token = token
			};


		}
		public async Task<UserResponse> GetUserDetailsAsync(int userId)
		{
			var user = await _userRepository.GetUserByIdAsync(userId);
			return _mapper.Map<UserResponse>(user);

		}


		public string GenerateToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

			List<Claim> claims = new()
			{
				new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()!),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Name, user.FirstName + ' ' + user.LastName),
				new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWT:Issuer"]!),
				new Claim(JwtRegisteredClaimNames.Aud, _configuration["JWT:Audience"]!),
			};

			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
			};

			var token = tokenHandler.CreateToken(tokenDescription);
			return tokenHandler.WriteToken(token);

		}

	}
}
