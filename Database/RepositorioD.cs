using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace TonyWebApplication
{
    /// <summary>
    /// Repositório usando Dapper
    /// </summary>
    public class RepositorioD : IRepositorio
    {
        public async Task<List<Caminhao>> Lista()
        {
            using (var db = new DatabaseContext())
            {
                var conn = db.Database.GetDbConnection();
                var Caminhoes = await conn.QueryAsync<Caminhao>("SELECT * FROM Caminhoes ORDER BY AnoFabricacao, AnoModelo, Modelo;");

                return Caminhoes.ToList();
            }
        }

        public async Task<Caminhao> Busca(Guid CaminhaoID)
        {
            using (var db = new DatabaseContext())
            {
                var conn = db.Database.GetDbConnection();

                var Caminhao = await conn.QueryAsync<Caminhao>("SELECT * FROM Caminhoes WHERE Id=@Id;", new { Id = CaminhaoID });

                return Caminhao.FirstOrDefault();
            }
        }

        public async Task Salva(Caminhao Caminhao)
        {
            using (var db = new DatabaseContext())
            {
                var conn = db.Database.GetDbConnection();

                if (Caminhao.Id == Guid.Empty)
                {
                    await conn.ExecuteAsync(
                    @"
                            INSERT INTO Caminhoes 
                            (Id, Modelo, AnoModelo, AnoFabricacao, Observacoes) 
                            VALUES
                            (@Id, @Modelo, @AnoModelo, @AnoFabricacao, @Observacoes);
                        ",
                        new
                        {
                            Caminhao.Id,
                            Caminhao.Modelo,
                            Caminhao.Observacoes,
                            Caminhao.AnoFabricacao,
                            Caminhao.AnoModelo
                        }
                    );
                }
                else
                {
                    await conn.ExecuteAsync(
                    @"
                            UPDATE Caminhoes 
                            SET
                                Modelo=@Modelo, 
                                AnoModelo=@AnoModelo, 
                                AnoFabricacao=@AnoFabricacao, 
                                Observacoes=@Observacoes  
                            WHERE 
                                Id=@Id
                            ;
                        ",
                        new
                        {
                            Caminhao.Id,
                            Caminhao.Modelo,
                            Caminhao.Observacoes,
                            Caminhao.AnoFabricacao,
                            Caminhao.AnoModelo
                        }
                    );
                }
            }
        }

        public async Task ApagaPorId(Guid CaminhaoID)
        {
            using (var db = new DatabaseContext())
            {
                var conn = db.Database.GetDbConnection();

                await conn.ExecuteAsync(
                    @"
                            DELETE FROM Caminhoes 
                            WHERE 
                                Id=@Id
                            ;
                        ",
                        new
                        {
                            CaminhaoID
                        }
                );
            }
        }

        public async Task Apaga(Caminhao Caminhao)
        {
            await ApagaPorId(Caminhao.Id);
        }

        public Task<Caminhao> Novo()
        {
            return Task.FromResult<Caminhao>(
                new Caminhao()
                {
                    Id = Guid.NewGuid(),
                    Observacoes = "Novo caminhão"
                });
        }
    }

}