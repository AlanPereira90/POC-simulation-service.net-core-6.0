using src.domain.simulation.interfaces;
using src.domain.common.interfaces;

using src.domain.simulation.services;
using src.domain.simulation.repositories;
using src.domain.infrastructure.database;
using src.domain.infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers((option) =>
{
  option.Filters.Add(new HttpResponseExceptionFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISimulationRepository, SimulationRepository>();
builder.Services.AddScoped<ISimulationService, SimulationService>();
builder.Services.AddScoped<IDatabase, DatabaseService>();
builder.Services.AddSingleton<DynamoDB>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
