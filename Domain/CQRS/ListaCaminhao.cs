using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS
{
    public class ListaCaminhaoRequest : IRequest<ListaCaminhaoResponse>
    {
    }

    public class ListaCaminhaoResponse : List<Caminhao>
    {
        public ListaCaminhaoResponse(List<Caminhao> lista):base(lista)
        {
            
        }
    }
}
