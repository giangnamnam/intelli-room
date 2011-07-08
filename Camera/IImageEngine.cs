using System;
namespace Camera
{
    public interface IImageEngine
    {
        event Action<LastResults> finishImageProcess;
        event Action<double> lowIluminanceEvent;
        event Action<double> highIluminanceEvent;
        LastResults LastResult { get; set; }
        event Action<double> movementDetected;
        event Action<FaceResult> peopleDetected;
        void StartEngine();
        void StopEngine();
    }
}
