using System;
using System.IO;
using System.Net;
using System.Text;

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
            get { return idticket; }
            set { idticket = value; }
        }
        private static bool modounico;

        public static bool ModoUnico
        {
            get { return modounico; }
            set { modounico = value; }
        }

        private static string caminhoFoto;
        public static string CaminhoFoto
        {
            get { return caminhoFoto; }
            set { caminhoFoto = value; }
        }


        //Função generica para registrar as ações do usuario
        public static void RegistrarLog(string Action)
        {
            if (Properties.Settings.Default["ArquivoAuditoria"].ToString() != "")
            {
                string data = DateTime.Now.ToShortDateString();
                string hora = DateTime.Now.ToLongTimeString();
                string maquina = Dns.GetHostName();
                //Escreve no arquivo
                string texto = data + " " + hora + " " + "(" + maquina + "):" + Action;

                string arquivo = Properties.Settings.Default["ArquivoAuditoria"].ToString() + @"\" + data.Replace("/", "-") + ".dat";

                using (StreamWriter tw = new StreamWriter(arquivo, File.Exists(arquivo) ? true : false))
                {
                    byte[] utf8String = Encoding.UTF8.GetBytes(texto);
                    tw.WriteLine(BitConverter.ToString(utf8String));
                    tw.Close();
                }

            }

        }

    }
}