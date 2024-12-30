using Dapper;
using System.Data;
using Web_API.Models;

namespace Web_API.Repositories
{
	public class ExamTypeRepository
	{
		private readonly IDbConnection _dbConnection;
		public ExamTypeRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<IEnumerable<ExamType>> GetAllTypes()
		{
			var query = @"SELECT * FROM ExamTypes";
			return await _dbConnection.QueryAsync<ExamType>(query);
		}

		public async Task<int> AddType(string TypeName)
		{
			var query = "INSERT INTO ExamTypes (Name) VALUES (@Name);" +
						"SELECT CAST(SCOPE_IDENTITY() as int)";
			return await _dbConnection.QuerySingleAsync<int>(query, new { Name = TypeName });
		}

		public async Task<int> UpdateType(int TypeID, string TypeName)
		{
			var query = "UPDATE ExamTypes" +
						"SET [Name] = @Name " +
						"WHERE ID = @ID";
			return await _dbConnection.ExecuteAsync(query, new { ID = TypeID, Name = TypeName });
		}
		public async Task<int> DeleteType(int TypeID)
		{
			var query = "DELETE FROM ExamTypes" +
						"WHERE ID = @ID";
			return await _dbConnection.ExecuteAsync(query, new { ID = TypeID });
		}




	}
}
