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
            DirectColor(Color.Black);
        }

        public void TurnOnLight()
        {
            DirectColor(Color.White);
        }

        //Directs Methods
        public void DirectColor(Color color)
        {
            DirectColor(color.R, color.G, color.B);
        }

        public void DirectColor(string colorName)
        {
            DirectColor(Color.FromName(colorName));
        }

        public void DirectColor(byte r, byte g, byte b)
        {
            var message = string.Format("DIRECT {0} {1} {2}", r, g, b);
            serialPort.Write(message);
        }

        //Degraded Methods
        public void GradientColor(Color color, int timeMillis)
        {
            GradientColor(color.R, color.G, color.B,timeMillis);
        }

        public void GradientColor(string colorName, int timeMillis)
        {
            GradientColor(Color.FromName(colorName),timeMillis);
        }

        public void GradientColor(byte r, byte g, byte b, int timeMillis)
        {
            var message = string.Format("GRADIENT {0} {1} {2} {3}", r, g, b, timeMillis);
            serialPort.Write(message);
        }

        //Random Methods
        public void RandomColor(int timeMillis)
        {
            var message = string.Format("RANDOM {0}",timeMillis);
            serialPort.Write(message);
        }

        public void DesactiveRandomColor()
        {
            serialPort.Write("RANDOM 0");
        }

        public void RandomColor(Boolean active, int timeMillis)
        {
            if (active)
            {
                RandomColor(timeMillis);
            }
            else
            {
                DesactiveRandomColor();
            }
        }
    }
}
