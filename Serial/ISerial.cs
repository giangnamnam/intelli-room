using System;
namespace Serial
{
    public interface ISerial
    {
        event EventHandler<System.IO.Ports.SerialDataReceivedEventArgs> serialReceived;
        void Write(byte[] data);
        void Write(string data);
    }
}
