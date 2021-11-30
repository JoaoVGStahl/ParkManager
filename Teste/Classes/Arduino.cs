using System;
using System.IO.Ports;
namespace Teste
{
    public class Arduino
    {
        private static SerialPort port;
        private static string portacom;

        public static bool entrada { get; set; }

        public static bool saida { get; set; }
        public static string PortaCom
        {
            set { portacom = value; }
        }
        public static SerialPort Port
        {
            set { port = value; }
        }
        public static void OpenCom()
        {
            if (!port.IsOpen)
            {
                try
                {
                    port.PortName = portacom;
                    port.Open();
                }
                catch
                {
                    throw;
                }
            }

        }
        public static void Inicializar()
        {
            try
            {
                OpenCom();
                WriteCom("I");
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void Parar()
        {
            try
            {
                if(port != null)
                {
                    if (port.IsOpen)
                    {
                        CloseCom();
                        WriteCom("P");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public static void CloseCom()
        {
            if (port.IsOpen)
            {
                try
                {
                    
                    port.Close();
                    port.Dispose();
                }
                catch
                {
                    throw;
                }
            }
        }
        public static void WriteCom(string method)
        {
            try
            {
                port.Write(method);
            }
            catch (Exception)
            {
                throw;
            }



        }
    }

}
