namespace Web_API.Models
{
	public class Exam : Patient
	{
		public int ID { get; set; }

		public string ExamTypeName { get; set; }
		public string Comments { get; set; }

		public string StatusName { get; set; }

		public DateOnly ExecuteDate { get; set; }
		public string UserName { get; set; }
	}
}
