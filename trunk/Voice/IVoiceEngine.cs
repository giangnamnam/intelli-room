using System;
using System.Collections.Generic;

namespace Voice
{
    public interface IVoiceEngine
    {
        void AddGrammar(System.Speech.Recognition.Grammar grammar);
        void ChangePrecisionRecognizer(int precision);
        void DeleteAllGrammars();
        void DictationMode();
        void LoadGrammar();
        void LoadListGrammar(List<string> list, string context);
        void AddListGrammar(List<string> list, string context);
        void Speak(string text);
        void Speak(string text, bool needSynthesizer);
        event EventHandler<System.Speech.Recognition.RecognitionEventArgs> speechRecognizer;
        
    }
}
