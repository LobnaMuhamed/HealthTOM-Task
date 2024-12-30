using Web_API.Models;
using Web_API.Models.DTOs.Requests;

namespace Web_API.Interfaces
{
	public interface IPatientService
	{
		Task<IEnumerable<Patient>> GetAllPatients();
		Task<Patient> GetPatientById(int ID);
		Task<int> AddNewPatientAsync(PatientRequest request);
		Task<bool> UpdatePatientAsync(int patientID, PatientRequest request);
		Task<bool> DeletePatientAsync(int patientID);

	}
}
