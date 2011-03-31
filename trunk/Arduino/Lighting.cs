using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Arduino
{
    public class Lighting : ILighting 
    {
        private Serial serialPort;

        public Lighting()
        {
            serialPort = new Serial();
        }

        public void TurnOffLight()
        {
            SetDirectColor(Color.Black);
        }

        public void TurnOnLight()
        {
            SetDirectColor(Color.White);
        }

        //Directs Methods
        public void SetDirectColor(Color color)
        {
            SetDirectColor(color.R, color.G, color.B);
        }

        public void SetDirectColor(string colorName)
        {
            SetDirectColor(Color.FromName(colorName));
        }

        public void SetDirectColor(byte r, byte g, byte b)
        {
            var message = string.Format("DIRECT {0} {1} {2}", r, g, b);
            serialPort.Write(message);
        }

        //Degraded Methods
        public void SetDegradedColor(Color color, byte time)
        {
            SetDegradedColor(color.R, color.G, color.B,time);
        }

        public void SetDegradedColor(string colorName, byte time)
        {
            SetDegradedColor(Color.FromName(colorName),time);
        }

        public void SetDegradedColor(byte r, byte g, byte b, byte time)
        {
            var message = string.Format("DEGRADED {0} {1} {2} {3}", r, g, b, time);
            serialPort.Write(message);
        }
    }
}
