using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS
{
    public class BuscaCaminhaoRequest : IRequest<BuscaCaminhaoResponse>
    {
        public Guid CaminhaoID { get; set; }
    }

    public class BuscaCaminhaoResponse : Caminhao
    {
        //public BuscaCaminhaoResponse(Caminhao caminhao) : base(caminhao)
        //{
        //}
    }
}
