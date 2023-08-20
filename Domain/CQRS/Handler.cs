using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CQRS
{
    public class ListaCaminhaoHandler : IRequestHandler<ListaCaminhaoRequest, ListaCaminhaoResponse>
    {
        IRepositorio _repositorio;
        public ListaCaminhaoHandler(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ListaCaminhaoResponse> Handle(ListaCaminhaoRequest request, CancellationToken cancellationToken)
        {
            var response = await _repositorio.Lista();

            return new ListaCaminhaoResponse(response);
        }
    }
    public class BuscaCaminhaoHandler : IRequestHandler<BuscaCaminhaoRequest, BuscaCaminhaoResponse>
    {
        IRepositorio _repositorio;
        public BuscaCaminhaoHandler(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<BuscaCaminhaoResponse> Handle(BuscaCaminhaoRequest request, CancellationToken cancellationToken)
        {
            var response = await _repositorio.Busca(request.CaminhaoID);

            return new BuscaCaminhaoResponse()
            {
                Id = response.Id,
                AnoFabricacao = response.AnoFabricacao,
                AnoModelo = response.AnoModelo,
                Modelo = response.Modelo,
                Observacoes = response.Observacoes
            };
        }
    }

    public class CreateCaminhaoHandler : IRequestHandler<CreateCaminhaoRequest, CreateCaminhaoResponse>
    {
        IRepositorio _repositorio;
        public CreateCaminhaoHandler(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<CreateCaminhaoResponse> Handle(CreateCaminhaoRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var response = new CreateCaminhaoResponse()
            {
                Id = Guid.NewGuid(),
                Observacoes = "Novo Caminhão"
            };

            return response;
        }
    }

    public class AtualizaCaminhaoHandler : IRequestHandler<AtualizaCaminhaoRequest, AtualizaCaminhaoResponse>
    {
        IRepositorio _repositorio;
        public AtualizaCaminhaoHandler(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<AtualizaCaminhaoResponse> Handle(AtualizaCaminhaoRequest request, CancellationToken cancellationToken)
        {
            await _repositorio.Salva(request);

            var response = new AtualizaCaminhaoResponse()
            {
            };

            return response;
        }
    }

    public class ApagaCaminhaoHandler : IRequestHandler<ApagaCaminhaoRequest, ApagaCaminhaoResponse>
    {
        IRepositorio _repositorio;
        public ApagaCaminhaoHandler(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<ApagaCaminhaoResponse> Handle(ApagaCaminhaoRequest request, CancellationToken cancellationToken)
        {
            await _repositorio.ApagaPorId(request.CaminhaoID);

            var response = new ApagaCaminhaoResponse()
            {
            };

            return response;
        }
    }
}
