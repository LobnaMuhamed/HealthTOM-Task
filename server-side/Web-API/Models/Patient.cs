namespace Web_API.Models
{
	public class Patient
	{
		public int PatientID { get; set; }

		public string FullName { get; set; }

		public char Gender { get; set; }

		public DateOnly BirthDate { get; set; }

		public string Email { get; set; }
	}
}
