using System;
namespace Arduino
{
    public interface ILighting
    {
        void ActiveRandomColorMode(int timeMillis);
        void DesactiveRandomColorMode();
        void RandomColorMode(bool active, int timeMillis);
        void SetGradientColor(byte r, byte g, byte b, int timeMillis);
        void SetGradientColor(System.Drawing.Color color, int timeMillis);
        void SetGradientColor(string colorName, int timeMillis);
        void SetDirectColor(byte r, byte g, byte b);
        void SetDirectColor(System.Drawing.Color color);
        void SetDirectColor(string colorName);
        void TurnOffLight();
        void TurnOnLight();
    }
}
