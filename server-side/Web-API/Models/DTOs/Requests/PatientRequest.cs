using System.ComponentModel.DataAnnotations;

namespace Web_API.Models.DTOs.Requests
{
	public class PatientRequest
	{
		[Required]
		public string FullName { get; set; }

		[Required]
		public bool Gender { get; set; }

		[Required]
		public DateOnly BirthDate { get; set; }

		[Required]
		public string Email { get; set; }
	}
}
