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
Teste Pr�tico 

Conhecimentos necess�rios: 
o	C# Rest Web Api com padr�es de projeto
o	Utiliza��o do Entity Framework
o	Utiliza��o do ASP.NET Core para cria��o de Web Api com Swagger
o	Utiliza��o do padr�o de projeto Repository
o	Utiliza��o do padr�o de projeto Dependency Injection
o	Utiliza��o do padr�o de projeto Inversion of Control
o	Utiliza��o do padr�o de projeto CQRS
o	Utiliza��o do padr�o DDD
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

#region // Exemplo com CQRS
app.MapGet("/CQRS/Lista", async (IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    var res = await mediator.Send(new ListaCaminhaoRequest());
    return res;
}).WithTags("CQRS");

app.MapGet("/CQRS/Busca", async (Guid CaminhaoID, IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    var res = await mediator.Send(new BuscaCaminhaoRequest() { CaminhaoID = CaminhaoID });
    return res;
}).WithTags("CQRS");

app.MapGet("/CQRS/Novo", async (IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    var res = await mediator.Send(new CreateCaminhaoRequest());
    return res;
}).WithTags("CQRS");

app.MapPost("/CQRS/Salva", async ([FromBody] AtualizaCaminhaoRequest request, IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    var res = await mediator.Send(request);
    return res;
}).WithTags("CQRS");

app.MapDelete("/CQRS/Apaga", async ([FromBody] ApagaCaminhaoRequest request, IRepositorio repositorio, [FromServices] IMediator mediator) =>
{
    var res = await mediator.Send(request);
    return res;
}).WithTags("CQRS");
#endregion

#region // Exemplo com modo "direto"
app.MapGet("/Lista", async (IRepositorio repositorio) =>
{
    return await repositorio.Lista();
}).WithTags("Tradicional");

app.MapGet("/Busca", async (Guid CaminhaoID, IRepositorio repositorio) =>
{
    return await repositorio.Busca(CaminhaoID);
}).WithTags("Tradicional");

app.MapGet("/Novo", async (IRepositorio repositorio) =>
{
    var res = await repositorio.Novo();
    return res;
}).WithTags("Tradicional");

app.MapPost("/Salva", async ([FromBody] Caminhao Caminhao, IRepositorio repositorio) =>
{
    await repositorio.Salva(Caminhao);
}).WithTags("Tradicional");

app.MapDelete("/Apaga", async (Guid CaminhaoID, IRepositorio repositorio) =>
{
    await repositorio.ApagaPorId(CaminhaoID);
}).WithTags("Tradicional");

#endregion

#endregion

app.Run();
