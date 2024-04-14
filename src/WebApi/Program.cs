using LightsOn.Application;
using LightsOn.WebApi;
using LightsOn.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyVaultIfConfigured(builder.Configuration);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseStaticFiles();

app.UseSwagger(c => {
    c.RouteTemplate = "/api/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "api";
});

app.MapEndpoints();

app.UsePathBase(new PathString("/api"));

app.Run();

namespace LightsOn.WebApi
{
    public partial class Program { }
}