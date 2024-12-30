using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Interfaces;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;

namespace Web_API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class PatientController : ControllerBase
	{
		private readonly IPatientService _service;
		public PatientController(IPatientService service)
		{
			_service = service;
		}

		[HttpGet("by-id/{id:int}")]
		public async Task<ActionResult<Patient>> GetPatientById(int id)
		{
			if (id <= 0)
				return BadRequest("Invalid Patient Id");

			var patient = await _service.GetPatientById(id);

			if (patient == null)
				return NotFound($"Patient with ID = {id} not found!");
			return Ok(patient);
		}
		[HttpGet("all")]
		public async Task<ActionResult<Patient>> GetAllPatients()
		{
			var patients = await _service.GetAllPatients();

			return Ok(patients);
		}

		[HttpPost("add-new-patient")]
		public async Task<ActionResult<int>> AddNewPatient(PatientRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var newPatientId = await _service.AddNewPatientAsync(request);

			if (newPatientId > 0)
				return CreatedAtAction(nameof(GetPatientById), new { id = newPatientId }, request);
			return BadRequest("Failed to add patient");
		}
		[HttpPut("update-patient/{id:int}")]
		public async Task<ActionResult> UpdatePatient(int id, PatientRequest request)
		{
			if (id <= 0)
				return BadRequest("Invalid Patient Id");
			var result = await _service.UpdatePatientAsync(id, request);

			if (result)
				return Ok("Patient updated successfully");
			return NotFound($"Patient with ID = {id} not found!");
		}
		[HttpDelete("delete-patient/{id:int}")]
		public async Task<ActionResult> DeletePatient(int id)
		{
			if (id <= 0)
				return BadRequest("Invalid Patient Id");
			var result = await _service.DeletePatientAsync(id);

			if (result)
				return Ok("Patient deleted successfully");
			return NotFound($"Patient with ID = {id} not found!");
		}

	}
}
