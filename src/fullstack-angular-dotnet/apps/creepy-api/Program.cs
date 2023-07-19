using CreepyApi.ExtentionMethods;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using CreepyApi.Layers.Infrastructure;
using CreepyApi.Layers.Application.Abstractions;
using CreepyApi.Layers.Core.Models;
using CreepyApi.Layers.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddSingleton<IGenericRepository<IDokument>, DokumentRepository>();
builder.Services.AddSingleton<IDokumenteService, DokumenteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || 1==1)
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(app.Environment);

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
