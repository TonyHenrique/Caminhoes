using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS
{
    public class AtualizaCaminhaoRequest : Caminhao, IRequest<AtualizaCaminhaoResponse>
    {
    }

    public class AtualizaCaminhaoResponse
    {
    }
}
