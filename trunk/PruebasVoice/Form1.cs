using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Speech.Recognition;

namespace PruebasVoice
{
    public partial class Form1 : Form
    {
        private Voice.VoiceEngine voice;

        public Form1()
        {
            InitializeComponent();
            voice = new Voice.VoiceEngine();
            voice.ChangePrecisionRecognizer(0);
            voice.speechRecognizer += new EventHandler<System.Speech.Recognition.RecognitionEventArgs>(voice_speechRecognizer);
        }

        void voice_speechRecognizer(object sender, System.Speech.Recognition.RecognitionEventArgs e)
        {
            recognizerBox.AppendText(e.Result.Text +" " + e.Result.Confidence * 100 + "%\n");
        }

        private void text(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new ThreadStart(Treakjd));
            t.Start();
        }

        private void priorityText(object sender, EventArgs e)
        {
            voice.Speak(SayBox.Text);
        }

        public void Treakjd()
        {
            voice.Speak(SayBox.Text, false);
        }

        private void addGrammar(object sender, EventArgs e)
        {
            Choices c1 = new Choices();
            c1.Add("encender");
            c1.Add("activar");
            Choices c2 = new Choices();
            c2.Add("la iluminación");
            c2.Add("la luz");
            Choices c3 = new Choices();
            c3.Add("poco a poco");
            c3.Add("muchisimo");
            GrammarBuilder grammarBuilder = new GrammarBuilder(c1);
            grammarBuilder.Append(c2);
            grammarBuilder.Append(c3);
            Grammar grammar = new Grammar(grammarBuilder);
            grammar.Name = "luz";
            voice.AddGrammar(grammar);
        }

        private void delgrammar(object sender, EventArgs e)
        {
            voice.DeleteAllGrammars();
        }

        private void dictationMode(object sender, EventArgs e)
        {
            voice.DictationMode();
        }
    }
}
