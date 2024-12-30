using Web_API.Models;
using Web_API.Models.DTOs.Requests;

namespace Web_API.Interfaces
{
	public interface IExamService
	{
		Task<IEnumerable<Exam>> GetAllExams();
		Task<int> AddNewExam(ExamRequest request);

		Task<bool> UpdateExam(int id, ExamRequest request);

		Task<bool> DeleteExam(int examID);

		Task<IEnumerable<Exam>> GetExamByPatientName(string patientName);

		Task<IEnumerable<Exam>> GetExamByPatientID(int patientID);

		Task<Exam> GetExamByID(int examID);

		Task<IEnumerable<Exam>> GetExamByName(string examName);

		Task<IEnumerable<Exam>> GetExamByDate(DateOnly date);

		Task<IEnumerable<ExamType>> GetAllExamTypes();

		Task<IEnumerable<ExamStatus>> GetAllExamStatuses();
	}
}
