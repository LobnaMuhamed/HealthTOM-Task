using Microsoft.AspNetCore.Identity;

namespace Web_API.Models.DTOs.Responses
{
	public class UserResponse
	{
		public int UserId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }


	}
}
