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

        public Task<CreateCaminhaoResponse> Handle(CreateCaminhaoRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateCaminhaoResponse()
            {
                Id = Guid.NewGuid(),
                Observacoes = "Novo Caminhão"
            };

            return Task.FromResult(response);
        }
    }

    public class AtualizaCaminhaoHandler : IRequestHandler<AtualizaCaminhaoRequest, AtualizaCaminhaoResponse>
    {
        public Task<AtualizaCaminhaoResponse> Handle(AtualizaCaminhaoRequest request, CancellationToken cancellationToken)
        {
            var response = new AtualizaCaminhaoResponse()
            {
            };

            return Task.FromResult(response);
        }
    }

    public class ApagaCaminhaoHandler : IRequestHandler<ApagaCaminhaoRequest, ApagaCaminhaoResponse>
    {
        public Task<ApagaCaminhaoResponse> Handle(ApagaCaminhaoRequest request, CancellationToken cancellationToken)
        {
            var response = new ApagaCaminhaoResponse()
            {
            };

            return Task.FromResult(response);
        }
    }
}
