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
            serialPort = SerialSingleton.Serial;
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
        public void SetGradientColor(Color color, int timeMillis)
        {
            SetGradientColor(color.R, color.G, color.B,timeMillis);
        }

        public void SetGradientColor(string colorName, int timeMillis)
        {
            SetGradientColor(Color.FromName(colorName),timeMillis);
        }

        public void SetGradientColor(byte r, byte g, byte b, int timeMillis)
        {
            var message = string.Format("GRADIENT {0} {1} {2} {3}", r, g, b, timeMillis);
            serialPort.Write(message);
        }

        //Random Methods
        public void ActiveRandomColorMode(int timeMillis)
        {
            var message = string.Format("RANDOM 1 {0}",timeMillis);
            serialPort.Write(message);
        }

        public void DesactiveRandomColorMode()
        {
            serialPort.Write("RANDOM 0");
        }

        public void RandomColorMode(Boolean active, int timeMillis)
        {
            if (active)
            {
                ActiveRandomColorMode(timeMillis);
            }
            else
            {
                DesactiveRandomColorMode();
            }
        }
    }
}
