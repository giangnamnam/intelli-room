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
            string PortCom = searchArduino();
            if (PortCom != "NULL")
            {
                serial = new SerialPort(PortCom, 9600) { NewLine = "\r\n" };
                serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serial_DataReceived);

                if (!serial.IsOpen)
                    serial.Open();
            }
            else
            {
                Message.ErrorMessage("Arduino no encontrado");
            }
        }

        /// <summary>
        /// Devuelve el nombre del puerto donde esta conectado arduino
        /// </summary>
        /// <returns>En caso de no existir devuelve "NULL"</returns>
        private string searchArduino()
        {
            string[] serialPortsName = SerialPort.GetPortNames();
            string portCom = "NULL";

            foreach (var elem in serialPortsName)
            {
                SerialPort serialPort = new SerialPort(elem, 9600) { NewLine = "\r\n" };
                serialPort.ReadTimeout = 500;
                bool res = false;
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    res = IsArduino(serialPort);
                    serialPort.Close();
                }
                else
                {
                    res = IsArduino(serialPort);
                }
                if (res)
                {
                    portCom = elem;
                    break;
                }
            }
            return portCom;
        }

        private bool IsArduino(SerialPort serialPort)
        {
            serialPort.WriteLine("CHECK");
            try
            {
                return serialPort.ReadLine() == "ACK";
            }
            catch (Exception)
            {
                return false;
            }
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