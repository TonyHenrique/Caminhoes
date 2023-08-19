using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Caminhao
    {
        public Guid Id { get; set; }
        /// <summary>
        /// (Poderá aceitar apenas FH e FM)
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// (Ano deverá ser o atual)
        /// </summary>
        public long AnoFabricacao { get; set; }

        /// <summary>
        /// (Poderá ser o atual ou o ano subsequente)
        /// </summary>
        public long AnoModelo { get; set; }

        public string Observacoes { get; set; }
    }
}
