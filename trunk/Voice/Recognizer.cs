using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;

namespace Voice
{
    class Recognizer
    {
        #region Fields

        /// <summary>
        /// Objeto de la libreria SAPI encargado del reconocimiento
        /// </summary>
        private SpeechRecognitionEngine speechRecognition;

        /// <summary>
        /// Booleano que dice si esta disponible el reconocimiento
        /// </summary>
        private bool isAvailable;

        /// <summary>
        /// Booleano que nos dice si estamos en modo dictado
        /// </summary>
        private bool dictationMode;

        /// <summary>
        /// Precision con la que queremos que salte el evento ante un filtro en la gramatica
        /// </summary>
        private int precision;

        /// <summary>
        /// Evento que se lanza cuando reconoce algo que esta dentro de la precision fijada
        /// </summary>
        public event EventHandler<SpeechRecognizedEventArgs> speechRecognized;//evento que salta cuando se cumplen las condiciones exigidas
        #endregion

        #region Builders
        internal Recognizer()
        {
            speechRecognition = new SpeechRecognitionEngine();
            DictationMode();
            InitRecognizer();
            this.precision = 70;
        }

        internal Recognizer(int precision)
        {
            speechRecognition = new SpeechRecognitionEngine();
            DictationMode();
            InitRecognizer();
            this.precision = precision;
        }

        internal Recognizer(Grammar grammar)
        {
            speechRecognition = new SpeechRecognitionEngine();
            AddGrammar(grammar);
            InitRecognizer();
            this.precision = 70;
        }

        internal Recognizer(Grammar grammar, int precision)
        {
            speechRecognition = new SpeechRecognitionEngine();
            AddGrammar(grammar);
            InitRecognizer();
            this.precision = precision;
        }

        private void InitRecognizer()
        {
            speechRecognition.SetInputToDefaultAudioDevice();
            isAvailable = false;
            speechRecognition.RecognizeAsync(RecognizeMode.Multiple);
            ActiveRecognizer();
        }

        #endregion

        #region Properties
        internal bool IsAvailable
        {
            get { return isAvailable; }
        }

        internal int Precision
        {
            get { return precision; }
            set { precision = value; }
        }
        #endregion

        #region Methods
        internal void AddGrammar(Grammar grammar)
        {
            if (dictationMode)
            {
                DeleteAllGrammars();
                dictationMode = false;
            }
            speechRecognition.LoadGrammar(grammar);
        }

        internal void DictationMode()
        {
            DeleteAllGrammars();
            speechRecognition.LoadGrammar(new DictationGrammar());
            dictationMode = true;
        }

        internal void DeleteAllGrammars()
        {
            dictationMode = false;
            speechRecognition.UnloadAllGrammars();
        }

        internal void InactiveRecognizer()
        {
            if (isAvailable)
            {
                isAvailable = false;
                speechRecognition.SpeechRecognized -= new EventHandler<SpeechRecognizedEventArgs>(speechRecognition_SpeechRecognized);
            }
        }

        internal void ActiveRecognizer()
        {
            if (!isAvailable)
            {
                isAvailable = true;
                speechRecognition.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speechRecognition_SpeechRecognized);
            }
        }

        void speechRecognition_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence * 100 >= precision)
            {
                speechRecognized(sender, e);
            }
        }
        #endregion
    }
}