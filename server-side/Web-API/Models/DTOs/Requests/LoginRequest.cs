using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web_API.Models.DTOs.Requests
{
	public class LoginRequest
	{
		[Required, EmailAddress]
		public string Email { get; set; }

		[Required, PasswordPropertyText]
		public string Password { get; set; }
	}
}
