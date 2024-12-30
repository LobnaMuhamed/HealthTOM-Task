using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web_API.Models.DTOs.Requests
{
	public class ExamRequest
	{
		[Required]
		public int PatientID { get; set; }

		[Required]
		public int ExamTypeID { get; set; }

		[AllowNull]
		public string? Comments { get; set; }

		[Required]
		public int ExamStatusID { get; set; }

		[Required]
		public DateOnly ExecuteDate { get; set; } = new DateOnly();

		public int CreatedBy { get; set; }
	}
}
