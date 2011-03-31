using System;
namespace Lighting
{
    public interface ILighting
    {
        void SetDegradedColor(byte r, byte g, byte b, byte time);
        void SetDegradedColor(System.Drawing.Color color, byte time);
        void SetDegradedColor(string colorName, byte time);
        void SetDirectColor(byte r, byte g, byte b);
        void SetDirectColor(System.Drawing.Color color);
        void SetDirectColor(string colorName);
        void TurnOffLight();
        void TurnOnLight();
    }
}
