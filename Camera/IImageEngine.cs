using System;
namespace Camera
{
    public interface IImageEngine
    {
        event EventHandler facesDetected;
        event EventHandler iluminanceEvent;
        event EventHandler imageResult;
        LastResults LastResult { get; set; }
        event EventHandler movementDetected;
        void StartEngine();
        void StopEngine();
    }
}
