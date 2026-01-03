using System.Reflection;
using Application.Models.Settings;
using ProjDependencyInjection.Extensions;
using ProjDependencyInjection.Mapster;
using GryphonProject.EndpointDefinitions;

var builder = WebApplication.CreateBuilder(args);
var projectName = Assembly.GetExecutingAssembly().GetName().Name;

builder.Services.ConfigureCommonServices(builder.Configuration, projectName!);

var app = builder.Build();

var mapster = app.Configuration.Get<MapsterConfiguration>();

var isDevelopment = app.Environment.IsDevelopment();

var securitySettings = app.Configuration.GetSection("Security").Get<SecuritySettings>();

app.ConfigureCommonPipeline(isDevelopment, mapster!, securitySettings!);

app.UseHttpsRedirection();

app.RegisterEndpoints();

app.Run();