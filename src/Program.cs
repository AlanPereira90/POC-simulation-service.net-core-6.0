using src.domain.simulation.interfaces;
using src.domain.common.interfaces;

using src.domain.simulation.services;
using src.domain.simulation.repositories;
using src.domain.infrastructure.database;
using src.domain.simulation.entities;
using src.domain.infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISimulationRepository, SimulationRepository>();
builder.Services.AddScoped<ISimulationService, SimulationService>();
builder.Services.AddScoped<IDatabase<Simulation>, DatabaseService<Simulation>>();
builder.Services.AddSingleton<DyanamoDB>();

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
