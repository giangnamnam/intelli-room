using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Arduino
{
    public class Serial
    {
        private SerialPort serial;
        public event EventHandler<SerialDataReceivedEventArgs> serialReceived;
        public object WriteMonitor = new Object();

        public Serial ()
        {
            serial = new SerialPort("COM4", 9600) { NewLine = "\r" };
            serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serial_DataReceived);
        }

        public Serial(int PuertoCOM)
        {
            serial = new SerialPort("COM"+PuertoCOM.ToString(), 9600) { NewLine = "\r" };
            serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serial_DataReceived);
        }

        void  serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
 	        serialReceived(sender,e);
        }

        public void Write(byte[] data)
        {
            try
            {
                Monitor.Enter(WriteMonitor);
                serial.Open();
                serial.Write(data, 0, data.Length);
                serial.Close();
                Monitor.Exit(WriteMonitor);
            }
            catch (Exception)
            {
               
            }
        }

        public void Write(string data)
        {
            try
            {
                Monitor.Enter(WriteMonitor);
                serial.Open();
                serial.WriteLine(data);
                serial.Close();
                Monitor.Exit(WriteMonitor);
            }
            catch (Exception)
            {
                
            }
        }
    }
}