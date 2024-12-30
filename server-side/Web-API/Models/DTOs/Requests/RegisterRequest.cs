using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Models.DTOs.Requests
{
	public class RegisterRequest
	{
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		public string LastName { get; set; } = string.Empty;

		[PasswordPropertyText]
		public string Password { get; set; } = string.Empty;
	}
}
