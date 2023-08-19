using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TonyWebApplication
{
    public interface IRepositorio
    {
        Task Apaga(Caminhao Caminhao);
        Task ApagaPorId(Guid CaminhaoID);
        Task<Caminhao> Busca(Guid CaminhaoID);
        Task<List<Caminhao>> Lista();
        Task Salva(Caminhao Caminhao);
    }
}