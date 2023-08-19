using Domain;
using Microsoft.AspNetCore.Mvc;
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
