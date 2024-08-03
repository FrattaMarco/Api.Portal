using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
if (builder.Environment.EnvironmentName == "Docker")
{
    builder.WebHost.UseUrls("https://*:443");
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ConfigureHttpsDefaults(listenOptions =>
        {
            listenOptions.ServerCertificate = new X509Certificate2("localhost.pfx", "PasswordTest");
        });
    });
}

builder.Configuration.AddJsonFile($"Ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api.ApiPortal", Version = "v1" });
});
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseOcelot().Wait();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.ApiPortal V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
