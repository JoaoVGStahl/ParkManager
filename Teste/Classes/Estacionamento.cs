using System;

namespace ParkManager
{
    public class Estacionamento
    {
        public static string cnpj { get; set; }

        public static string razao_social { get; set; }

        public static string endereco { get; set; }

        public static string bairro { get; set; }

        public static int numero { get; set; }

        public static string cidade { get; set; }

        public static string estado { get; set; }
        public static string cep { get; set; }

        public static string telefone { get; set; }

        public static TimeSpan tolerancia { get; set; }

        public static decimal valor_hr { get; set; }

        public static decimal valor_min { get; set; }

        public static decimal valor_unico { get; set; }

        public static string caminho_foto_padrao { get; set; }
    }
}
