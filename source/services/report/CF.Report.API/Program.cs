using CF.CustomMediator.Configuration;
using CF.Report.API.Configurations;
using System.Text.Json.Serialization;

WebApplicationBuilder appBuilder = WebApplication.CreateBuilder(args);

appBuilder.Configuration
    .SetBasePath(appBuilder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{appBuilder.Environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

appBuilder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

appBuilder.Services.AddMediator(typeof(Program));
appBuilder.Services.AddAutoMapper(typeof(Program));
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddSwagger();
appBuilder.Services.AddQueryDatabase(appBuilder.Configuration);
appBuilder.Services.AddApplicationSettings(appBuilder.Configuration);
appBuilder.Services.AddApplicationServices();
appBuilder.Services.AddMessageBroker(appBuilder.Configuration);
appBuilder.Services.AddQueryDatabase();

WebApplication app = appBuilder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();