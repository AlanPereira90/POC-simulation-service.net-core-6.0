using src.application.controllers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapMethods(ReadinessController.Route, ReadinessController.Methods, ReadinessController.Handle);

app.Run();
