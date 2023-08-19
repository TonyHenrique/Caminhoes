using Domain;
using Microsoft.AspNetCore.Mvc;
using TonyWebApplication;
using static System.Net.Mime.MediaTypeNames;

/*
// Tony - Minimal API
Teste Prático 

Conhecimentos necessários: 
o	C# Rest Web Api com padrões de projeto
o	Utilização do Entity Framework
o	Utilização do ASP.NET Core para criação de Web Api com Swagger
o	Utilização do padrão de projeto Repository
o	Utilização do padrão de projeto Dependency Injection
o	Utilização do padrão de projeto Inversion of Control
o	Utilização do padrão de projeto CQRS
o	Utilização do padrão DDD
*/

#region Builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepositorio>(new RepositorioEF() { });

var app = builder.Build();
#endregion

#region App
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
#endregion

#region Endpoints

app.MapGet("/Lista", (RepositorioEF repositorio) =>
{
    return repositorio.Lista();
});

app.MapGet("/Busca", (Guid CaminhaoID, RepositorioEF repositorio) =>
{
    return repositorio.Busca(CaminhaoID);
});

app.MapPost("/Salva", async ([FromBody] Caminhao Caminhao, RepositorioEF repositorio) =>
{
    await repositorio.Salva(Caminhao);
});

app.MapDelete("/Apaga", async (Guid CaminhaoID, RepositorioEF repositorio) =>
{
    await repositorio.ApagaPorId(CaminhaoID);
});

#endregion

app.Run();
