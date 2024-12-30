using Dapper;
using System.Data;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;

namespace Web_API.Repositories
{
	public class PatientRepository
	{
		private readonly IDbConnection _dbConnection;
		public PatientRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<Patient> GetPatientById(int id)
		{
			var query = @"
						SELECT PatientID, FullName, 
							CASE
								WHEN Gender = 1 THEN 'M' 
								WHEN Gender = 0 THEN 'F' 
								ELSE 'Unknown'
							END As Gender,
							Email, BirthDate
						FROM Patients
						WHERE PatientID = @PatientID;
				";
			Patient? patient = await _dbConnection.QuerySingleOrDefaultAsync<Patient>(query, new { PatientID = id });
			return patient!;
		}

		public async Task<IEnumerable<Patient>> GetAllPatients()
		{
			var query = @"
						SELECT PatientID, FullName, 
							CASE
								WHEN Gender = 1 THEN 'M' 
								WHEN Gender = 0 THEN 'F' 
								ELSE 'Unknown'
							END As Gender,
							Email, BirthDate
						FROM Patients;
				";
			return await _dbConnection.QueryAsync<Patient>(query);
		}
		public async Task<int> AddNewPatientAsync(PatientRequest request)
		{
			var query = @"
				INSERT INTO Patients (FullName, Gender, Email, BirthDate)
				VALUES (@FullName, @Gender , @Email, @BirthDate);
				SELECT CAST(SCOPE_IDENTITY() as int);
			";
			return await _dbConnection.QuerySingleAsync<int>(query, request);
		}

		public async Task<int> UpdatePatientAsync(int id, PatientRequest request)
		{
			var query = @"UPDATE Patients 
						SET FullName = @FullName,
							Gender = @Gender, 
							BirthDate = @BirthDate, 
							Email = @Email
							Where PatientID = @PatientID;";

			return await _dbConnection.ExecuteAsync(query,
			new
			{
				PatientID = id,
				request.FullName,
				request.Gender,
				request.BirthDate,
				request.Email
			});
		}

		public async Task<int> DeletePatientAsync(int id)
		{
			var query = @"Delete From Patients WHERE PatientID = @PatientID;";
			return await _dbConnection.ExecuteAsync(query, new { PatientID = id });
		}
	}
}
