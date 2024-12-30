using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web_API.Models;

namespace Web_API.Extensions
{
	public static class IdentityExtensions
	{

		public static IServiceCollection CofigureIdentityOptions(this IServiceCollection services)
		{
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.User.RequireUniqueEmail = true;
			});
			return services;
		}

		public static IServiceCollection AddIdenetityAuth(this IServiceCollection services, IConfiguration configuration)
		{
			var JWTSettings = configuration.GetSection("JWT");
			services.AddAuthentication(options =>
			{
				options.DefaultChallengeScheme =
				options.DefaultScheme =
				options.DefaultAuthenticateScheme =
				JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidAudience = JWTSettings["Audience"],
					ValidIssuer = JWTSettings["Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.GetSection("Key").Value!))
				};
			});
			return services;
		}

		public static WebApplication AddIdenetityAuthMiddleWares(this WebApplication app)
		{
			app.ConfigureCORS()
				.UseAuthentication()
				.UseAuthorization();
			return app;
		}



	}
}
