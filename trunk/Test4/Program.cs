using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Test4
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPort sp = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);

            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);

            sp.Open();

            byte[] data = { (byte)'H', (byte)'o', (byte)'l', (byte)'a', (byte)'!' };
            while (true)
            {
                sp.Write(data, 0, data.Length);
                System.Threading.Thread.Sleep(1000);
            }

            sp.Close();
        }

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;

            String s = "";

            while (sp.BytesToRead > 0)
                s += (char)sp.ReadChar();

            Console.WriteLine(s);
        }
    }
}
