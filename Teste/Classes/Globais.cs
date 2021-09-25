using System;
using System.IO;
using System.Net;

namespace Teste
{
    public static class Globais
    {
        //Obtem o id do usuario
        private static int idusuario;

        public static int IdUsuario
        {
            get { return idusuario; }
            set { idusuario = value; }
        }
        //Obtem o Login
        private static string login;

        public static string Login
        {
            get { return login; }
            set { login = value; }
        }
        //Obtem o nivel de acesso do usuario
        private static int nivel;

        public static int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        //Obtem o status do usuario
        private static int userstatus;

        public static int UserStatus
        {
            get { return userstatus; }
            set { userstatus = value; }
        }
        //Obtem o Valor da Hora
        private static decimal valorhora;

        public static decimal ValorHora
        {
            get { return valorhora; }
            set { valorhora = value; }
        }
        //Obtem o tempo de tolerancia
        private static TimeSpan tolerancia;

        public static TimeSpan Tolerencia
        {
            get { return tolerancia; }
            set { tolerancia = value; }
        }
        private static int idticket;

        public static int IdTicket
        {
            get {  return idticket; }
            set { idticket = value; }
        }
        //Função generica para registrar as ações do usuario
        public static void RegistrarLog(string Action)
        {
            string caminho = Properties.Settings.Default["ArquivoAuditoria"].ToString();
            if (caminho != "")
            {
                //Define o caminho e escreve no arquivp
                using (StreamWriter outputFile = new StreamWriter(Properties.Settings.Default.ArquivoAuditoria, true))
                {
                    //Obtem a data atual, hora e a máquina.
                    string data = DateTime.Now.ToShortDateString();
                    string hora = DateTime.Now.ToLongTimeString();
                    string maquina = Dns.GetHostName();
                    //Escreve no arquivo
                    outputFile.WriteLine(data + " " + hora + " " + "(" + maquina + "):" + Action);
                }
            }
            
        }
    }
}


