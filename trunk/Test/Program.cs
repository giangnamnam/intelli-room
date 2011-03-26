using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechRecognitionEngine recognition = new SpeechRecognitionEngine();
            recognition.SetInputToDefaultAudioDevice();
            //lo que dicen estas pruebas,es que puedo generar un numero de opciones grandes, de tal forma
            //de que puedo generar frases del estilo "choice1" + "choice2" + "choice3" pero no de la forma
            //choice2 + "choice1" + "choice 3"
            //ademas de esto, las pruebas dicen que por encima del 70,72 % se trata de un valor correcto
            Choices choices = new Choices();
            choices.Add("encender");
            choices.Add("activar");
            Choices choices2 = new Choices();
            choices2.Add("la iluminación");
            choices2.Add("la luz");
            Choices choices3 = new Choices();
            choices3.Add("poco a poco");
            choices3.Add("muchisimo");
            //aqui debes appendear por orden
            GrammarBuilder grammarBuilder = new GrammarBuilder(choices);
            grammarBuilder.Append(choices2);
            grammarBuilder.Append(choices3);
            
            Grammar grammar = new Grammar(grammarBuilder);
            grammar.Name = "luz";
            //OTRA GRAMATICA
            Choices c1 = new Choices();
            c1.Add("El coche");
            c1.Add("El carraco");
            Choices c2 = new Choices();
            c2.Add("tumba agujas");
            c2.Add("la esta montando");
            c2.Add("esta flipandolo");
            Choices c3 = new Choices();
            c3.Add("por Baraca");
            c3.Add("por la citi");
            c3.Add("a tope");
            c3.Add("a tope con la cope");
            GrammarBuilder gb2 = new GrammarBuilder(c1);
            gb2.Append(c2);
            gb2.Append(c3);
            Grammar grammarCoches = new Grammar(gb2);
            grammarCoches.Name = "carracos";

            recognition.LoadGrammar(grammar);
            recognition.LoadGrammar(grammarCoches);
            //recognition.LoadGrammar(new DictationGrammar());

            recognition.RecognizeAsync(RecognizeMode.Multiple);
            recognition.SpeechRecognized += new EventHandler<System.Speech.Recognition.SpeechRecognizedEventArgs>(recognition_SpeechRecognized);



            Console.ReadLine();
        }

        static void recognition_SpeechRecognized(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence * 100 > 70)
            {
                Console.WriteLine("Dijo: " + e.Result.Text);
                Console.WriteLine("Con una seguridad del: " + e.Result.Confidence * 100 + "%");
                Console.WriteLine("De la gramatica: " + e.Result.Grammar.Name);
                Console.WriteLine("----");
            }

        }
    }
}
