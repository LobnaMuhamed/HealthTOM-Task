using Microsoft.Data.SqlClient;
using System.Data;
using Web_API.Interfaces;
using Web_API.Repositories;
using Web_API.Services;

namespace Web_API.Extensions
{
	public static class DapperExtensions
	{
		public static IServiceCollection InjectDbConnection(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<IDbConnection>(s => new SqlConnection
			(config.GetConnectionString("DefaultConnection")));
			return services;
		}

		public static IServiceCollection InjectRepositories(this IServiceCollection services)
		{
			services.AddScoped<UserRepository>();
			services.AddScoped<PatientRepository>();
			services.AddScoped<ExamRepository>();
			services.AddScoped<ExamTypeRepository>();
			services.AddScoped<ExamStatusRepository>();


			return services;
		}

		public static IServiceCollection InjectServicesAndInterfaces(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<IUserContext, UserContextService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IPatientService, PatientService>();
			services.AddScoped<IExamService, ExamService>();

			return services;
		}
	}
}
