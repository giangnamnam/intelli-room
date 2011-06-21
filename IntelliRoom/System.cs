using Arduino;
using Media;
using Voice;
using Camera;

namespace IntelliRoom
{
    static class IntelliRoomSystem
    {
        public static IVoiceEngine voiceEngine;
        public static ILighting lighting;
        public static IDevice device;
        public static IMediaPlayer media;
        public static InterpreterSpeech speechInterpreter;
        public static IImageEngine camera;
        public static Events events;
        public static Programmer progammer;

        public static void InitSystem()
        {
            voiceEngine = new VoiceEngine();
            lighting = new Lighting();
            device = new Device();
            media = new Media.MediaPlayer();
            speechInterpreter = new InterpreterSpeech();
            camera = new ImageEngine();
            events = new Events();
            progammer = new Programmer();
        }
    }
}
