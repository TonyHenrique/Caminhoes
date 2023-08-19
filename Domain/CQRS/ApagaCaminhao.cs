using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS
{
    public class ApagaCaminhaoRequest : IRequest<ApagaCaminhaoResponse>
    {
        public Guid CaminhaoID { get; set; }
    }

    public class ApagaCaminhaoResponse
    {
    }
}
