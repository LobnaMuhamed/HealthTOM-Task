using Web_API.Interfaces;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;
using Web_API.Repositories;

namespace Web_API.Services
{
	public class PatientService : IPatientService
	{
		private readonly PatientRepository _repository;
		public PatientService(PatientRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Patient>> GetAllPatients()
		{
			return await _repository.GetAllPatients();
		}

		public async Task<Patient> GetPatientById(int id)
		{
			return await _repository.GetPatientById(id);
		}
		public async Task<int> AddNewPatientAsync(PatientRequest request)
		{
			try
			{
				var newPatientId = await _repository.AddNewPatientAsync(request);
				return newPatientId;
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Error adding new patient", ex);
			}
		}
		public async Task<bool> UpdatePatientAsync(int id, PatientRequest request)
		{
			return await _repository.UpdatePatientAsync(id, request) > 0;
		}
		public async Task<bool> DeletePatientAsync(int patientID)
		{
			return await _repository.DeletePatientAsync(patientID) > 0;
		}


	}
}
