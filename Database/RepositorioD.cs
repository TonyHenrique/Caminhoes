using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace TonyWebApplication
{
    /// <summary>
    /// Repositório usando Dapper
    /// </summary>
    public static class RepositorioD
    {
        public static async Task<List<Caminhao>> Lista()
        {
            using (var db = new DatabaseContext())
            {
                var conn = db.Database.GetDbConnection();
                var Caminhoes = await conn.QueryAsync<Caminhao>("SELECT * FROM Caminhoes ORDER BY AnoFabricacao, AnoModelo, Modelo;");

                return Caminhoes.ToList();
            }
        }

        public static async Task<Caminhao> Busca(Guid CaminhaoID)
        {
            using (var db = new DatabaseContext())
            {
                var conn = db.Database.GetDbConnection();

                var Caminhao = await conn.QueryAsync<Caminhao>("SELECT * FROM Caminhoes WHERE Id=@Id;", new { Id = CaminhaoID });

                return Caminhao.FirstOrDefault();
            }
        }

        public static async Task Salva(Caminhao Caminhao)
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

        public static async Task Apaga(Caminhao Caminhao)
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
                            Caminhao.Id
                        }
                );
            }

        }
    }

}