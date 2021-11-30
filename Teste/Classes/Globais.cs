using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ParkManager
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
                        string TextEncoded = EncodeToUtf8(texto);
                        tw.WriteLine(TextEncoded);
                        tw.Close();
                    }

                }
                else
                {
                    VerificaDiretorio();
                }
            }

        }
        public static void VerificaDiretorio()
        {
            if (!Directory.Exists(Properties.Settings.Default.ArquivoAuditoria) && Properties.Settings.Default.ArquivoAuditoria != null)
            {
                Directory.CreateDirectory(Properties.Settings.Default.ArquivoAuditoria);
            }
            if (!Directory.Exists(@"C:\ParkManager\ticket"))
            {
                Directory.CreateDirectory(@"C:\ParkManager\ticket");
            }
            if (!Directory.Exists(@"C:\ParkManager\assets"))
            {
                Directory.CreateDirectory(@"C:\ParkManager\assets");
            }
            if (!Directory.Exists(Estacionamento.caminho_foto_padrao) && Estacionamento.caminho_foto_padrao != null)
            {
                Directory.CreateDirectory(Estacionamento.caminho_foto_padrao);
            }
        }

        public static void GerenciarLogs()
        {
            if (!File.Exists(ArquivoAud))
            {
                using (StreamWriter tw = File.CreateText(ArquivoAud))
                {
                    string InitialTexto = "Criado em: " + DateTime.Now.ToShortDateString();
                    string TextEncoded = EncodeToUtf8(InitialTexto);
                    tw.WriteLine(TextEncoded);
                    tw.Close();
                }
            }
            ApagarAntigos();

        }
        public static string EncodeToUtf8(string texto)
        {
            var utf8String = Encoding.UTF8.GetBytes(texto);
            return Convert.ToBase64String(utf8String);
        }
        public static string DecodeToString(string value)
        {
            var Decode = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(Decode);
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
        public static string EncodeToMd5(string text)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();

        }
    }
}
