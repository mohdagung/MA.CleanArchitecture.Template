using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using MA.Clean.Template.Infrastructure;
using MA.Clean.Template.Api.Middleware;
using MA.Clean.Template.Application.Samples.Commands; // <- referensi assembly Application
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// API Versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// Swagger
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MA Clean Template API", Version = "v1" });
});

// MediatR v11 style: cukup sebutkan assembly yang berisi handler
builder.Services.AddMediatR(typeof(CreateSample.Handler).Assembly);

// Infrastructure
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseErrorHandling();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.Run();
