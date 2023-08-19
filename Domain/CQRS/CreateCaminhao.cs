using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS
{
    public class CreateCaminhaoRequest : IRequest<CreateCaminhaoResponse>
    {
    }

    public class CreateCaminhaoResponse : Caminhao
    {
    }
}
