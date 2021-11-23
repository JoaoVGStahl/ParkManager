using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
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
        public static string ArquivoAud { get; set; }


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

                if (File.Exists(ArquivoAud))
                {

                    using (StreamWriter tw = new StreamWriter(ArquivoAud, true))
                    {
                        byte[] TextEncoded = EncodeToUtf8(texto);
                        tw.WriteLine(BitConverter.ToString(TextEncoded));
                        tw.Close();
                    }

                }
            }

        }
        public static void GerenciarLogs()
        {
            if (!File.Exists(ArquivoAud))
            {
                using (StreamWriter tw = File.CreateText(ArquivoAud))
                {
                    string InitialTexto = "Criado em: " + DateTime.Now.ToShortDateString();
                    byte[] TextEncoded = EncodeToUtf8(InitialTexto);
                    tw.WriteLine(BitConverter.ToString(TextEncoded));
                    tw.Close();

                }
            }
            ApagarAntigos();

        }
        public static byte[] EncodeToUtf8(string texto)
        {
            byte[] utf8String = Encoding.UTF8.GetBytes(texto);
            return utf8String;
        }
        private static void ApagarAntigos()
        {
            DirectoryInfo di = new DirectoryInfo(Properties.Settings.Default["ArquivoAuditoria"].ToString());

            foreach (FileInfo file in di.GetFiles())
            {
                if (file.CreationTime < DateTime.Now.AddDays(-7))
                {
                    file.Delete();
                }
            }
        }

    }
}
