using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Voice;
using Arduino;
using Media;

namespace IntelliRoom
{
    static class IntelliRoomSystem
    {
        public static Language language = new Spanish();
        public static IVoiceEngine voiceEngine = new VoiceEngine();
        public static ILighting lighting = new Lighting.Lighting();
        public static IMediaPlayer media = new Media.MediaPlayer();
        public static InterpreterSpeech speechInterpreter = new InterpreterSpeech();
    }
    static class Config
    {
        public static bool sleep = false;
        public static int portComArduino = 4;
    }
    static class Directories
    {
        public static string GetAlarmXML()
        {
            return Directory.GetCurrentDirectory() + "\\Data\\" + "Alarms.xml";
        }

        public static string GetDirectoryLanguage()
        {
            return Directory.GetCurrentDirectory() + "\\Language";
        }

        public static string GetLanguageXML()
        {
            return Directory.GetCurrentDirectory() + "\\Language\\" + IntelliRoomSystem.language.GetLanguaje()+ ".xml";
        }

        public static string GetDirectoryGrammar()
        {
            return Directory.GetCurrentDirectory() + "\\Grammar";
        }

        public static string GetGrammarXML()
        {
            return Directory.GetCurrentDirectory() + "\\Grammar\\" + IntelliRoomSystem.language.GetLanguaje() + ".xml";
        }
    }
}
