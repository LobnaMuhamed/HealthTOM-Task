using Dapper;
using System.Data;
using Web_API.Models;

namespace Web_API.Repositories
{
	public class ExamStatusRepository
	{
		private readonly IDbConnection _dbConnection;
		public ExamStatusRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<IEnumerable<ExamStatus>> GetAllStatus()
		{
			var query = @"SELECT * FROM ExamStatus";
			return await _dbConnection.QueryAsync<ExamStatus>(query);
		}
	}
}




