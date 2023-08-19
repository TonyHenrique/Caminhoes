using Entidades;
using Microsoft.AspNetCore.Mvc;
using TonyWebApplication;

// Tony - Minimal API

#region Builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapGet("/Lista", () =>
{
    return RepositorioEF.Lista();
});

app.MapGet("/Busca", (Guid CaminhaoID) =>
{
    return RepositorioEF.Busca(CaminhaoID);
});

app.MapPost("/Salva", async ([FromBody] Caminhao Caminhao) =>
{
    await RepositorioEF.Salva(Caminhao);
});

app.MapDelete("/Apaga", async (Guid CaminhaoID) =>
{
    await RepositorioEF.ApagaPorId(CaminhaoID);
});

#endregion

app.Run();
