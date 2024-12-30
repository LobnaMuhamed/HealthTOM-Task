using Dapper;
using Web_API.Extensions;
using static Web_API.Helpers.DateTypeHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddSwaggerExplorer()
	.InjectDbConnection(builder.Configuration)
	.InjectRepositories()
	.InjectServicesAndInterfaces()
	.CofigureIdentityOptions()
	.AddIdenetityAuth(builder.Configuration);
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


app.ConfigureSwaggerExplorer()
	.UseHttpsRedirection();


app.AddIdenetityAuthMiddleWares();

app.MapControllers();


app.Run();
