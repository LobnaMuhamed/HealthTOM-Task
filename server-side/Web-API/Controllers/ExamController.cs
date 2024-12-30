using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API.Interfaces;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;

namespace Web_API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ExamController : ControllerBase
	{
		private readonly IExamService _examService;
		private readonly IUserContext _userContext;
		public ExamController(IExamService examService, IUserContext userContext)
		{
			_examService = examService;
			_userContext = userContext;
		}
		[HttpGet("all", Name = "GetAllExams")]
		public async Task<ActionResult<IEnumerable<Exam>>> GetAllExams()
		{
			var exams = await _examService.GetAllExams();
			return Ok(exams);

		}


		[HttpGet("by-date/{date:datetime}", Name = "GetExamByDate")]
		public async Task<ActionResult<IEnumerable<Exam>>> GetExamByDate(DateOnly date)
		{
			var exam = await _examService.GetExamByDate(date);
			if (exam == null)
				return NotFound($"There is no Exams in date = {date}");
			return Ok(exam);
		}

		[HttpGet("by-id/{id:int}")]
		public async Task<ActionResult<Exam>> GetExamByID(int id)
		{
			if (id <= 0) return BadRequest($"Invalid Id = {id}");
			var exam = await _examService.GetExamByID(id);
			if (exam == null)
				return NotFound($"Exam with Id = {id} not found!");
			return Ok(exam);
		}
		[HttpGet("by-name/{name}", Name = "GetExamByName")]
		public async Task<ActionResult<Exam>> GetExamByName(string name)
		{
			if (name == null) return BadRequest($"Invalid Exam Type Name");
			var exam = await _examService.GetExamByName(name);
			if (exam == null)
				return NotFound($"Exam with name = {name} not found!");
			return Ok(exam);
		}

		[HttpGet("by-patient-id/{id:int}", Name = "GetExamsByPatientID")]
		public async Task<ActionResult<IEnumerable<Exam>>> GetExamsByPatientID(int id)
		{
			if (id <= 0) return BadRequest($"Invlid Id = {id}");
			var exams = await _examService.GetExamByPatientID(id);
			if (exams == null)
				return NotFound($"There is No Exams with Patient Id = {id}");
			return Ok(exams);
		}

		[HttpGet("by-patient-name/{name}", Name = "GetExamsByPatientName")]
		public async Task<ActionResult<IEnumerable<Exam>>> GetExamsByPatientName(string name)
		{
			if (name == null) return BadRequest($"Invalid Patient Name");
			var exams = await _examService.GetExamByPatientName(name);
			if (exams == null)
				return NotFound($"There is No Exams with Patient Name = {name}");
			return Ok(exams);
		}
		[HttpGet("get-all-types")]
		public async Task<ActionResult<IEnumerable<ExamType>>> GetAllExamTypes()
		{
			var exams = await _examService.GetAllExamTypes();
			return Ok(exams);
		}
		[HttpGet("get-all-status")]
		public async Task<ActionResult<IEnumerable<ExamStatus>>> GetAllExamStatus()
		{
			var exams = await _examService.GetAllExamStatuses();
			return Ok(exams);
		}


		[HttpPost("add-new-exam")]
		public async Task<ActionResult<Exam>> AddNewExam(ExamRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			request.CreatedBy = _userContext.GetCurrentUserId();
			var newExamId = await _examService.AddNewExam(request);
			if (newExamId > 0)
				return CreatedAtAction(nameof(GetExamByID), new { id = newExamId }, request);
			return BadRequest("Failed to add exam");
		}

		[HttpPut("update-exam/{id:int}")]
		public async Task<ActionResult> UpdateExam(int id, ExamRequest request)
		{
			if (id <= 0)
				return BadRequest($"Invalid Exam ID = {id}");
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			request.CreatedBy = _userContext.GetCurrentUserId();
			var result = await _examService.UpdateExam(id, request);
			if (result)
				return Ok("Exam updated successfully :-)");
			return BadRequest("Failed to update exam");
		}

		[HttpDelete("delete-exam/{id:int}")]
		public async Task<ActionResult> DeleteExam(int id)
		{
			if (id <= 0)
				return BadRequest($"Invalid exam ID = {id}");
			var result = await _examService.DeleteExam(id);
			if (result)
				return Ok("Exam deleted successfully :-)");
			return BadRequest("Failed to delete exam");
		}
	}
}
