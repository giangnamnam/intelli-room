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
            String result = "";
            
            string[] commands = SeparateCommands(e.Result.Grammar.Name);
            foreach (string command in commands)
            {
                result += Command(e.Result.Grammar.Name) +", ";
            }

            IntelliRoomSystem.voiceEngine.Speak(result);
            Data.InfoMessages.InformationMessage("Se ejecutó por comando de voz: " + e.Result.Grammar.Name + " .Con la frase: " + e.Result.Text + " .Devolviendo: " + result);
        }

        private String Command(String command)
        {
            String[] separateCommand = SeparateArguments(command);
            MethodInfo[] methods = Reflection.SearchSpeakMethod(separateCommand[0]);
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
                        object resultObj = Reflection.Invoke(mi, parametres);
                        if (resultObj != null)
                            result = resultObj.ToString();
                        break; //para no ejecutar mas de uno
                    }
                }
            }
            return result;
        }

        private String[] SeparateArguments(String command)
        {
            String[] result = command.Split(' ');
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Replace("_", " ");
            }
            return result;
        }

        private String[] SeparateCommands(String str)
        {
            String[] result = str.Split('|');
            return result;
        }
    }
}
