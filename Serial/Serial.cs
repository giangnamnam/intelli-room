using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Serial
{
    public class Serial : ISerial
    {
        private SerialPort serial;
        public event EventHandler<SerialDataReceivedEventArgs> serialReceived;

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
            serial.Open();
            serial.Write(data, 0, data.Length);
            serial.Close();
        }

        public void Write(string data)
        {
            serial.Open();
            serial.WriteLine(data);
            serial.Close();
        } 
        
    }
}