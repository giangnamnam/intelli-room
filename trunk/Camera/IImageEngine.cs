using System;
namespace Camera
{
    public interface IImageEngine
    {
        event Action<LastResults> finishImageProcess;
        event Action<double> iluminanceEvent;
        LastResults LastResult { get; set; }
        event Action<double> movementDetected;
        event Action<FaceResult> peopleDetected;
        void StartEngine();
        void StopEngine();
    }
}
