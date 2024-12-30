using System.Security.Claims;
using Web_API.Interfaces;

namespace Web_API.Services
{
	public class UserContextService : IUserContext
	{
		private readonly IHttpContextAccessor _httpContext;

		public UserContextService(IHttpContextAccessor httpContext)
		{
			_httpContext = httpContext;
		}
		public int GetCurrentUserId()
		{
			var user = _httpContext.HttpContext?.User;

			return Int32.Parse(user!.FindFirstValue(ClaimTypes.NameIdentifier)!);
		}
	}
}
