using Arduino;
using Media;
using Voice;

namespace IntelliRoom
{
    static class IntelliRoomSystem
    {
        public static IVoiceEngine voiceEngine = new VoiceEngine();
        public static ILighting lighting = new Lighting();
        public static IDevice device = new Device();
        public static IMediaPlayer media = new Media.MediaPlayer();
        public static InterpreterSpeech speechInterpreter = new InterpreterSpeech();
    }


}
