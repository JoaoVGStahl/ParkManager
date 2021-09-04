using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    class Globais
    {
        private string ConnString { get; set; }

        private string Login { get; set; }

        private int Nivel { get; set; }

        private int LoginStatus { get; set; }

        private decimal ValorHora { get; set; }

        private TimeSpan Tolerancia { get; set; }

        private int QuantidadeVagas { get; set; }

        private int VagaAtuais { get; set; }

        private int StatusEstacionamento { get; set; }
    }
}
