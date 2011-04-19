using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino
{
    public class Device : IDevice
    {
        private Serial serialPort;
        
        public Device()
        {
            serialPort = SerialSingleton.Serial;
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
