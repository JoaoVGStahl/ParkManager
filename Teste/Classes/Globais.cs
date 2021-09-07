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

        private int UserStatus { get; set; }

        private decimal ValorHora { get; set; }

        private TimeSpan Tolerancia { get; set; }

        private int QuantidadeVagas { get; set; }

        private int VagaAtuais { get; set; }

        private int StatusEstacionamento { get; set; }

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
    }
}
