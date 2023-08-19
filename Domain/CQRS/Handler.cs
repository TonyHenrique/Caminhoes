using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CQRS
{
    public class CreateCaminhaoHandler : IRequestHandler<CreateCaminhaoRequest, CreateCaminhaoResponse>
    {
        IRepositorio _repositorio;
        public CreateCaminhaoHandler(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<CreateCaminhaoResponse> Handle(CreateCaminhaoRequest request, CancellationToken cancellationToken)
        {
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
