using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Web_API.Models;

namespace Web_API.Repositories
{
	public class UserRepository
	{
		private readonly IDbConnection _dbConnection;
		public UserRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<int> RegisterUserAsync(User user)
		{

			var query = @"
					INSERT INTO Users (FirstName, LastName, Email, Password)
					VALUES (@FirstName, @LastName, @Email, @Password);
					SELECT CAST(SCOPE_IDENTITY() as int);
					";
			return await _dbConnection.QuerySingleAsync<int>(query, user);
		}
		public async Task<User?> GetUserByEmailAsync(string email)
		{
			string query = "SELECT * FROM Users WHERE Email = @Email;";
			return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
		}
		public async Task<User?> GetUserByIdAsync(int Id)
		{
			string query = "SELECT * FROM Users WHERE UserId = @UserId;";
			return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { UserId = Id });
		}


	}
}
