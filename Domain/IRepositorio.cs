using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepositorio
    {
        Task<Caminhao> Novo();
        Task Salva(Caminhao Caminhao);
        Task Apaga(Caminhao Caminhao);
        Task ApagaPorId(Guid CaminhaoID);
        Task<List<Caminhao>> Lista();
        Task<Caminhao> Busca(Guid CaminhaoID);
    }
}