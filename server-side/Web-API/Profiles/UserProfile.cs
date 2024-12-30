using AutoMapper;
using Web_API.Models;
using Web_API.Models.DTOs.Requests;
using Web_API.Models.DTOs.Responses;

namespace Web_API.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<RegisterRequest, User>();
			CreateMap<User, UserResponse>();

		}
	}
}
