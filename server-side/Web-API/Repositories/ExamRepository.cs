using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;

namespace Web_API.Repositories
{
	public class ExamRepository
	{
		private readonly IDbConnection _dbConnection;
		public ExamRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<int> AddNewExam(ExamRequest request)
		{
			var query = @"

			INSERT INTO Exams (PatientID, ExamTypeID, Comments ,ExamStatusID ,CreatedBy ,ExecuteDate)
				 VALUES
					   (@PatientID
					   ,@ExamTypeID
					   ,@Comments
					   ,@ExamStatusID
					   ,@CreatedBy
					   ,@ExecuteDate);
						SELECT CAST(SCOPE_IDENTITY() as int);";
			return await _dbConnection.QuerySingleAsync<int>(query, request);
		}

		public async Task<int> UpdateExam(int id, ExamRequest request)
		{
			var query = @"UPDATE Exams
						   SET PatientID = @PatientID
							  ,ExamTypeID = @ExamTypeID
							  ,Comments = @Comments
							  ,ExamStatusID = @ExamStatusID
							  ,CreatedBy = @CreatedBy
							  ,ExecuteDate = @ExecuteDate
							WHERE ID = @ID;";
			return await _dbConnection.ExecuteAsync(query, new
			{
				ID = id,
				request.PatientID,
				request.ExamTypeID,
				request.Comments,
				request.ExamStatusID,
				request.CreatedBy,
				request.ExecuteDate
			});
		}

		public async Task<int> DeleteExamAsync(int examID)
		{
			var query = @"DELETE FROM Exams WHERE ID = @ID";
			return await _dbConnection.ExecuteAsync(query, new { ID = examID });
		}

		public async Task<IEnumerable<Exam>> GetAllExams()
		{
			var query = @"select * from GetAllExams";
			return await _dbConnection.QueryAsync<Exam>(query);
		}
		public async Task<IEnumerable<Exam>> GetExamByPatientName(string patientName)
		{
			var parameters = new { PatientName = patientName };
			var exams = await _dbConnection.QueryAsync<Exam>(
				"GetExamByPatientName",
				parameters,
				commandType: CommandType.StoredProcedure);
			return exams ?? Enumerable.Empty<Exam>();
		}

		public async Task<IEnumerable<Exam>> GetExamByPatientID(int patientId)
		{
			var parameters = new { PatientID = patientId };
			var exams = await _dbConnection.QueryAsync<Exam>(
				"GetExamByPatientId",
				parameters,
				commandType: CommandType.StoredProcedure);
			return exams ?? Enumerable.Empty<Exam>();
		}

		public async Task<Exam> GetExamByID(int examId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("ExamID", 1);
			var exams = await _dbConnection.QueryFirstOrDefaultAsync<Exam>(
				"GetExamByID",
				parameters,
				commandType: CommandType.StoredProcedure);
			return exams!;
		}

		public async Task<IEnumerable<Exam>> GetExamByName(string examName)
		{
			var parameters = new { ExamName = examName };
			var exams = await _dbConnection.QueryAsync<Exam>(
				"GetExamByName",
				parameters,
				commandType: CommandType.StoredProcedure);
			return exams;
		}


		public async Task<IEnumerable<Exam>> GetExamByDate(DateOnly examDate)
		{
			var parameters = new { ExamDate = examDate };
			var exams = await _dbConnection.QueryAsync<Exam>(
				"GetExamByDate",
				parameters,
				commandType: CommandType.StoredProcedure);
			return exams;
		}
	}
}
