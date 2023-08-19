using Domain;
using Domain.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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

builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblyContaining<Program>();
    c.RegisterServicesFromAssemblyContaining<CreateCaminhaoHandler>();
});

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

app.MapGet("/Lista", (IRepositorio repositorio) =>
{
    return repositorio.Lista();
});

app.MapGet("/Busca", (Guid CaminhaoID, IRepositorio repositorio) =>
{
    return repositorio.Busca(CaminhaoID);
});

// Exemplo simples com CQRS
app.MapGet("/CQRS/Novo", async (IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    var res = await mediator.Send(new CreateCaminhaoRequest());

    return res;
});

app.MapPost("/CQRS/Salva", async ([FromBody] AtualizaCaminhaoRequest Caminhao, IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    await mediator.Send(Caminhao);
});

app.MapDelete("/CQRS/Apaga", async ([FromBody] ApagaCaminhaoRequest Caminhao, IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    await mediator.Send(Caminhao);
});

// Exemplo com modo "direto"
app.MapGet("/Novo", async (IRepositorio repositorio) =>
{
    return await repositorio.Novo();
});

app.MapPost("/Salva", async ([FromBody] Caminhao Caminhao, IRepositorio repositorio) =>
{
    await repositorio.Salva(Caminhao);
});

app.MapDelete("/Apaga", async (Guid CaminhaoID, IRepositorio repositorio) =>
{
    await repositorio.ApagaPorId(CaminhaoID);
});

#endregion

app.Run();
