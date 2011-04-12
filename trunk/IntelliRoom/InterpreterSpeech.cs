using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voice;
using System.Reflection;
using System.Speech.Recognition;

namespace IntelliRoom
{
    class InterpreterSpeech
    {
        public InterpreterSpeech()
        {
            IntelliRoomSystem.voiceEngine.LoadGrammar();
            IntelliRoomSystem.voiceEngine.speechRecognizer += new EventHandler<RecognitionEventArgs>(speechRecognition);
        }

        void speechRecognition(object sender, RecognitionEventArgs e)
        {
            String result = Command(e.Result.Grammar.Name);
            //IntelliRoomSystem.voiceEngine.Speak(result);
        }

        private String Command(String command)
        {
            String[] separateCommand = SeparateString(command);
            MethodInfo[] methods = Reflection.SearchMethod(separateCommand[0]);
            String result = "";
            //sacamos los parametros
            string[] parametres = new string[separateCommand.Length - 1];

            for (int i = 1; i < separateCommand.Length; i++)
            {
                parametres[i - 1] = separateCommand[i];
            }

            if (methods != null)
            {
                //hay al menos un metodo con ese nombre
                foreach (MethodInfo mi in methods)
                {
                    if (mi.GetParameters().Length == separateCommand.Length - 1)
                    {
                        //hay un metodo con el mismo numero de parametros
                        result = Reflection.Invoke(mi, parametres);
                        break; //para no ejecutar mas de uno
                    }
                }
            }
            return result;
        }

        private String[] SeparateString(String str)
        {
            String[] result = str.Split(new char[] { ' ' });
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Replace("_", " ");
            }
            return result;
        }
    }
}
