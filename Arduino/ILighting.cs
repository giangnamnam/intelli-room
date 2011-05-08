using System;
namespace Arduino
{
    public interface ILighting
    {
        void RandomColor(int timeMillis);
        void DesactiveRandomColor();
        void RandomColor(bool active, int timeMillis);
        void GradientColor(byte r, byte g, byte b, int timeMillis);
        void GradientColor(System.Drawing.Color color, int timeMillis);
        void GradientColor(string colorName, int timeMillis);
        void DirectColor(byte r, byte g, byte b);
        void DirectColor(System.Drawing.Color color);
        void DirectColor(string colorName);
        void TurnOffLight();
        void TurnOnLight();
    }
}
