using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino
{
    public class Device : IDevice
    {
        private Serial serialPort;
        private Dictionary<int, string> disposivos;
        
        public Device()
        {
            serialPort = SerialSingleton.Serial;
            disposivos = new Dictionary<int, string>();
        }

        public void SwitchOn (int numDisp)
        {
            serialPort.Write("SWITCHON "+numDisp.ToString());
        }

        public void SwitchOff (int numDisp)
        {
            serialPort.Write("SWITCHOFF "+numDisp.ToString());
        }

    }
}
