using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using Data;

namespace Arduino
{
    public class SerialSingleton
    {
        private static Serial serial;

        public static Serial Serial
        {
            get 
            {
                if (serial == null)
                {
                    serial = new Serial();
                }
                return serial;
            }
        }
    }

    public class Serial
    {
        private SerialPort serial;
        public event EventHandler<SerialDataReceivedEventArgs> serialReceived;
        public object WriteMonitor = new Object();

        public Serial ()
        {
            serial = new SerialPort("COM" + Data.Config.portComArduino.ToString(), 9600) { NewLine = "\r" };
            serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serial_DataReceived);
            //if(!serial.IsOpen) serial.Open();
        }

        void  serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
 	        serialReceived(sender,e);
        }

        public void Write(string data)
        {
            try
            {
                Monitor.Enter(WriteMonitor);
                serial.WriteLine(data);
            }
            catch (Exception)
            {
                //enviar un mensaje de error al sistema de errores
            }
            finally
            {
                Monitor.Exit(WriteMonitor);
            }
        }
    }
}