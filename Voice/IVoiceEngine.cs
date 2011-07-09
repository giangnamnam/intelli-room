using System;
namespace Voice
{
    public interface IVoiceEngine
    {
        void AddGrammar(System.Speech.Recognition.Grammar grammar);
        void AddGrammar(string url);
        void AddListGrammar(System.Collections.Generic.List<string> list, string context);
        void ChangePrecisionRecognizer(int precision);
        void DeleteAllGrammars();
        void DictationMode();
        void LoadGrammar();
        void LoadGrammar(string url);
        void LoadListGrammar(System.Collections.Generic.List<string> list, string context);
        void Speak(string text);
        void Speak(string text, bool needSynthesizer);
        event EventHandler<System.Speech.Recognition.RecognitionEventArgs> speechRecognizer;
    }
}
