using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

WebApplicationBuilder appBuilder = WebApplication.CreateBuilder(args);

appBuilder.Configuration
    .SetBasePath(appBuilder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{appBuilder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"configuration.{appBuilder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

string authenticationProviderKey = "EncapsulatedAuthKey";
byte[] key = Encoding.ASCII.GetBytes(appBuilder.Configuration.GetSection("JwtAuthenticationSettings:SecretKey").Value);

appBuilder.Services.AddAuthentication().AddJwtBearer(authenticationProviderKey, bearerOptions =>
{
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = appBuilder.Configuration.GetSection("JwtAuthenticationSettings:Audience").Value,
        ValidIssuer = appBuilder.Configuration.GetSection("JwtAuthenticationSettings:Issuer").Value
    };
});


appBuilder.Services.AddOcelot(appBuilder.Configuration);

WebApplication app = appBuilder.Build();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("API Gateway funcionando");
    });
});

app.UseOcelot().Wait();

app.Run();
