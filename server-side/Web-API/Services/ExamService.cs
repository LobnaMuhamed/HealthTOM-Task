using Microsoft.AspNetCore.Http.HttpResults;
using Web_API.Interfaces;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;
using Web_API.Repositories;

namespace Web_API.Services
{
	public class ExamService : IExamService
	{
		private readonly ExamRepository _repository;
		private readonly ExamTypeRepository _examTypeRepository;
		private readonly ExamStatusRepository _examStatusRepository;
		public ExamService(ExamRepository repository, ExamTypeRepository examTypeRepository, ExamStatusRepository examStatusRepository)
		{
			_repository = repository;
			_examTypeRepository = examTypeRepository;
			_examStatusRepository = examStatusRepository;
		}
		public async Task<int> AddNewExam(ExamRequest request)
		{
			return await _repository.AddNewExam(request);
		}

		public async Task<IEnumerable<Exam>> GetAllExams()
		{
			return await _repository.GetAllExams();
		}
		public async Task<IEnumerable<Exam>> GetExamByDate(DateOnly date)
		{
			return await _repository.GetExamByDate(date);
		}

		public async Task<Exam> GetExamByID(int examId)
		{
			return await _repository.GetExamByID(examId);
		}

		public async Task<IEnumerable<Exam>> GetExamByName(string examName)
		{
			return await _repository.GetExamByName(examName);
		}

		public async Task<IEnumerable<Exam>> GetExamByPatientID(int patientID)
		{
			return await _repository.GetExamByPatientID(patientID);
		}

		public async Task<IEnumerable<Exam>> GetExamByPatientName(string patientName)
		{
			return await _repository.GetExamByPatientName(patientName);
		}

		public async Task<bool> UpdateExam(int id, ExamRequest request)
		{
			return await _repository.UpdateExam(id, request) > 0;
		}

		public async Task<bool> DeleteExam(int examID)
		{
			return await _repository.DeleteExamAsync(examID) > 0;
		}

		public async Task<IEnumerable<ExamType>> GetAllExamTypes()
		{
			return await _examTypeRepository.GetAllTypes();
		}

		public async Task<IEnumerable<ExamStatus>> GetAllExamStatuses()
		{
			return await _examStatusRepository.GetAllStatus();
		}
	}
}
