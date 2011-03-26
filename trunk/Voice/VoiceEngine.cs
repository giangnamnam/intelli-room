using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;

namespace Voice
{
    /// <summary>
    /// Gestiona tareas automaticamente y tiene una "interface" para mejorar la abstraccion del proyecto
    /// </summary>
    public class VoiceEngine : Voice.IVoiceEngine
    {
        #region Fields
        private Synthesizer speech;
        private Recognizer recognizer;

        /// <summary>
        /// Evento que salta cuando reconoce algo con una precision definida
        /// </summary>
        public event EventHandler<RecognitionEventArgs> speechRecognizer;
        #endregion

        #region Builders
        public VoiceEngine()
        {
            speech = new Synthesizer();
            recognizer = new Recognizer();
            recognizer.speechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_speechRecognized);
            speech.finishSpeechEvent += new EventHandler<System.Speech.Synthesis.SpeakCompletedEventArgs>(speech_finishSpeech);
        }
        public VoiceEngine(int precisionRecognizer)
        {
            speech = new Synthesizer();
            recognizer = new Recognizer(precisionRecognizer);
            recognizer.speechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_speechRecognized);
            speech.finishSpeechEvent += new EventHandler<System.Speech.Synthesis.SpeakCompletedEventArgs>(speech_finishSpeech);
        }
        public VoiceEngine(Grammar grammar, int precisionRecognizer)
        {
            speech = new Synthesizer();
            recognizer = new Recognizer(grammar,precisionRecognizer);
            recognizer.speechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_speechRecognized);
            speech.finishSpeechEvent += new EventHandler<System.Speech.Synthesis.SpeakCompletedEventArgs>(speech_finishSpeech);
        }
        #endregion

        #region Methods
        public void AddGrammar(Grammar grammar)
        {
            recognizer.AddGrammar(grammar);
        }

        public void DictationMode()
        {
            recognizer.DictationMode();
        }

        public void DeleteAllGrammars()
        {
            recognizer.DeleteAllGrammars();
        }

        public void ChangePrecisionRecognizer(int precision)
        {
            if (precision >= 0 & precision <= 100)
            {
                recognizer.Precision = precision;
            }
        }

        /// <summary>
        /// Reproduce en el sintetizador el texto
        /// </summary>
        /// <param name="text">El texto a sintetizar</param>
        /// <param name="needSynthesizer">Puedes indicar que si se esta usando el sintetizador se lea o no la frase</param>
        public void Speak(String text, bool needSynthesizer)
        {
            recognizer.InactiveRecognizer();
            if (needSynthesizer)
                speech.SpeakTextQueue(text);
            else
                speech.SpeakText(text);
        }

        /// <summary>
        /// Metodo que encola por defecto
        /// </summary>
        /// <param name="text"></param>
        public void Speak(String text)
        {
            recognizer.InactiveRecognizer();
            speech.SpeakTextQueue(text);
        }
        #endregion

        #region Events
        void speech_finishSpeech(object sender, System.Speech.Synthesis.SpeakCompletedEventArgs e)
        {
            recognizer.ActiveRecognizer();
        }

        void recognizer_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
             this.speechRecognizer(sender, e);
        }
        #endregion

    }
}
