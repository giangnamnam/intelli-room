using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino
{
    class Electric
    {
        private Serial serialPort;
        private Dictionary<int, string> disposivos;
        
        public Electric()
        {
            serialPort = new Serial();
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
