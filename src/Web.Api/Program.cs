using System.Reflection;
using Application;
using Application.Services;
using Domain.Services;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Quartz;
using Serilog;
using Web.Api;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddQuartzHostedService();

builder.Services.Configure<EncryptionOptions>(builder.Configuration.GetSection("EncryptionOptions"));
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.MapEndpoints(app.MapGroup("api"));

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.ApplyMigrations();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.MapControllers();

await app.RunAsync();

namespace Web.Api
{
    public class Program;
}
