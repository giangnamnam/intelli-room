using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;

namespace Voice
{
    class Synthesizer
    {
        #region Fields

        /// <summary>
        /// Objeto de la libreria SAPI encargado de sintetizar
        /// </summary>
        private SpeechSynthesizer speechSynthesizer;

        /// <summary>
        /// Indica si se esta usando en sistetizador
        /// </summary>
        private Boolean inUse;

        /// <summary>
        /// Es una cola para ir almacenando trabajo por sintetizar
        /// </summary>
        private Queue<String> queueSpeech;

        /// <summary>
        /// Evento que salta cuando deja de sintetizar todo lo que este en la cola
        /// </summary>
        internal event EventHandler<SpeakCompletedEventArgs> finishSpeechEvent;
        #endregion

        #region Builders
        public Synthesizer()
        {
            speechSynthesizer = new SpeechSynthesizer();
            inUse = false;
            queueSpeech = new Queue<string>();
            speechSynthesizer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(finishSpeaking);
        }
        #endregion      

        #region Properties
        internal SpeechSynthesizer Speaker
        {
            get { return speechSynthesizer; }
            set { speechSynthesizer = value; }
        }

        internal Boolean InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sintetiza un Texto, si esta ocupado el sistetizador no se sitetizara
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal Boolean SpeakText(String text)
        {
            Boolean res = false;
            if(!InUse)
            {
                InUse = true;
                speechSynthesizer.SpeakAsync(text);
                res = true;
            }
            return res;
        }
        /// <summary>
        /// Sintetiza un texto, si esta ocupado el sistetizador lo deja encolado para mas tarde
        /// </summary>
        /// <param name="text"></param>
        internal void SpeakTextQueue(String text)
        {
            if (InUse)
            {
                queueSpeech.Enqueue(text);
            }
            else
            {
                InUse = true;
                speechSynthesizer.SpeakAsync(text);
            }
        }

        /// <summary>
        /// Cola vacia
        /// </summary>
        /// <returns></returns>
        private Boolean EmptyQueue()
        {
            return queueSpeech.Count == 0;
        }

        /// <summary>
        /// Cita el proximo texto en cola
        /// </summary>
        private void SpeakNextTextInQueue()
        {
            SpeakText(queueSpeech.Dequeue());
        }

        void finishSpeaking(object sender, SpeakCompletedEventArgs e)
        {
 	        InUse=false;
            if (!EmptyQueue())
            {
                SpeakNextTextInQueue();
            }
            else
            {
                finishSpeechEvent(sender, e);
            }
        }
        #endregion
    }
}
