using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Entidades;

namespace ProvaDevNet
{
    public static class Validacoes
    {
        /// <summary>
        /// As propriedades mínimas do caminhão deverão ser:
        /// * Modelo(Poderá aceitar apenas FH e FM)
        /// * Ano de Fabricação(Ano deverá ser o atual)
        /// * Ano Modelo(Poderá ser o atual ou o ano subsequente)
        /// </summary>
        public static bool ValidaCaminhao(this Caminhao c)
        {
            // Verificações...
            // Modelo
            if (!(c.Modelo == "FH" || c.Modelo == "FM"))
                throw new Exception("Modelo (Poderá aceitar apenas FH e FM)");

            // Ano Atual e Ano Subsequente
            var AnoAtual = DateTime.Now.Year;
            var AnoSubsequente = AnoAtual + 1;

            if (c.AnoFabricacao != AnoAtual)
                throw new Exception($"Ano de Fabricação (Ano deverá ser o atual {AnoAtual})");

            // Ano Modelo
            if (!((c.AnoModelo == AnoAtual) || (c.AnoModelo == AnoSubsequente)))
                throw new Exception($"Ano Modelo (Poderá ser o atual {AnoAtual} ou o ano subsequente {AnoSubsequente})");

            // Se chegamos até aqui, então está tudo certo!
            return true;
        }
    }
}
