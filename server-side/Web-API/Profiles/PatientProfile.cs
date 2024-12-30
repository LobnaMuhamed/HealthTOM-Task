using AutoMapper;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;
using Web_API.Models.DTOs.Responses;

namespace Web_API.Profiles
{
	public class PatientProfile : Profile
	{
		public PatientProfile()
		{
			CreateMap<PatientRequest, Patient>();

		}
	}
}
