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
    /// Repositório usando Entity Framework
    /// </summary>
    public static class RepositorioEF
    {
        public static async Task<List<Caminhao>> Lista()
        {
            using (var db = new DatabaseContext())
            {
                var Caminhoes = await
                                    db.Caminhoes
                                        .OrderBy(b => b.AnoFabricacao)
                                        .ThenBy(b => b.AnoModelo)
                                        .ThenBy(b => b.Modelo)
                                        .ToListAsync();

                return Caminhoes;
            }
        }

        public static async Task<Caminhao> Busca(Guid CaminhaoID)
        {
            using (var db = new DatabaseContext())
            {
                var Caminhao = await
                                    db.Caminhoes
                                        .Where(b => b.Id == CaminhaoID)
                                        .FirstOrDefaultAsync();

                return Caminhao;
            }
        }

        public static async Task Salva(Caminhao Caminhao)
        {
            using (var context = new DatabaseContext())
            {
                if (Caminhao.Id == Guid.Empty)
                {
                    context.Caminhoes.Add(Caminhao);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(Caminhao);
                    await context.SaveChangesAsync();

                }
            }
        }
        public static async Task ApagaPorId(Guid CaminhaoID)
        {
            using (var context = new DatabaseContext())
            {
                var caminhao = context.Find<Caminhao>(CaminhaoID);
                context.Remove(caminhao);

                await context.SaveChangesAsync();
            }
        }

        public static async Task Apaga(Caminhao Caminhao)
        {
            using (var context = new DatabaseContext())
            {
                context.Remove(Caminhao);

                await context.SaveChangesAsync();
            }
        }

    }
}
