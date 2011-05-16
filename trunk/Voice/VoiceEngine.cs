using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using System.Xml;
using Data;

namespace Voice
{
    /// <summary>
    /// Gestiona tareas automaticamente y tiene una "interface" para mejorar la abstraccion del proyecto
    /// </summary>
    public class VoiceEngine : IVoiceEngine
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
        #endregion

        #region Methods
        public void AddGrammar(Grammar grammar)
        {
            recognizer.AddGrammar(grammar);
        }

        public void LoadGrammar()
        {
            //eliminamos toda la gramatica
            DeleteAllGrammars();
            //cargamos el documento XML
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(Directories.GetGrammarXML());

                foreach (XmlNode command in xml.ChildNodes[1].ChildNodes)
                {
                    AddGrammar(LoadCommand(command));
                }
            }
            catch (Exception)
            {
                Message.ErrorMessage("No se ha encontrado el archivo de gramática: "+Directories.GetGrammarXML()+", no se cargará la gramática de voz");
            }
            
        }

        private Grammar LoadCommand(XmlNode commandNode)
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();


            foreach (XmlNode choice in commandNode.ChildNodes)
            {
                grammarBuilder.Append(LoadChoices(choice));
            }

            Grammar command = new Grammar(grammarBuilder);
            //introduzco nombre de la gramatica
            command.Name = commandNode.Attributes[0].Value.ToString();

            return command;

        }

        private Choices LoadChoices(XmlNode choiceNode)
        {
            Choices choices = new Choices();

            foreach (XmlNode element in choiceNode.ChildNodes)
            {
                if (element.FirstChild == null)
                {
                    choices.Add(" ");
                }
                else
                {
                    choices.Add(element.FirstChild.InnerText);
                }
            }

            return choices;
        }

        public void DeleteAllGrammars()
        {
            recognizer.DeleteAllGrammars();
        }

        public void DictationMode()
        {
            recognizer.DictationMode();
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
