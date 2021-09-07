using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Teste
{
    class Globais
    {
        private string ConnString { get; set; }

        private int IdUsuario { get; set; }
        private string Login { get; set; }

        private int Nivel { get; set; }

        private int UserStatus { get; set; }

        private decimal ValorHora { get; set; }

        private TimeSpan Tolerancia { get; set; }

        private int QuantidadeVagas { get; set; }

        private int VagaAtuais { get; set; }

        private int StatusEstacionamento { get; set; }

        public string CaminhoArquivoLog { get; set; }

        public void setIdUsuario(int IdUsuario)
        {
            this.IdUsuario = IdUsuario;
        }
        public int getIdUsuario(int IdUsuario)
        {
            return this.IdUsuario;
        }
        public void setLogin(string Login)
        {
            this.Login = Login;
        }
        public string getLogin(string Login)
        {
            return this.Login;
        }
        public void setNivel(int Nivel)
        {
            this.Nivel = Nivel;
        }
        public int getNivel(int Nivel)
        {
            return this.Nivel;
        }
        public void setUserStatus(int UserStatus)
        {
            this.UserStatus = UserStatus;
        }
        public int getUserStatus(int UserStatus)
        {
            return this.UserStatus;
        }

        public void RegistrarLog(string Action)
        {
            using(StreamWriter outputFile = new StreamWriter("log.dat", true))
            {
                string data = DateTime.Now.ToShortDateString();
                string hora = DateTime.Now.ToLongTimeString();
                string maquina = Dns.GetHostName();

                outputFile.WriteLine(data + " " + hora + " " + "(" + maquina + "): " + Action);
            }
        }
    }
}
