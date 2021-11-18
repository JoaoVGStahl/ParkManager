using System;
using System.IO.Ports;
namespace Teste
{
    public class Arduino
    {
        private static SerialPort port;
        private static string portacom;

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
                    return;
                }
            }

        }
        public static void CloseCom()
        {
            if (port.IsOpen)
            {
                try
                {
                    port.PortName = portacom;
                    port.Close();
                }
                catch
                {
                    return;
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
