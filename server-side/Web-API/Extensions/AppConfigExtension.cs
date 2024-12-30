using Microsoft.Extensions.Options;

namespace Web_API.Extensions
{
	public static class AppConfigExtension
	{

		public static WebApplication ConfigureCORS(this WebApplication app)
		{
			app.UseCors(options => options.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod());
			return app;


		}
	}
}
