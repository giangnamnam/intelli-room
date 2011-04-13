using System;
namespace Arduino
{
    public interface IDevice
    {
        void SwitchOff(int numDisp);
        void SwitchOn(int numDisp);
    }
}
