using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using IntelliRoom;

namespace GUI
{
    public class CommandInterpreter
    {
        public String CommandsInterpreter(String toInterpreter)
        {
            toInterpreter = toInterpreter.ToLower();
            String result = "";

            string[] commands = SeparateCommands(toInterpreter);
            foreach (string command in commands)
            {
                if (ExitsCommand(command))
                {
                    result += ExecuteCommand(command) + "\n";
                }
                else
                {
                    result += "No existe comando: "+command;
                }
            }

            return result;
        }

        private String ExecuteCommand(String command)
        {
            String[] separateCommand = SeparateArguments(command);
            MethodInfo[] methods = Reflection.SearchMethod(separateCommand[0]);
            String result = "null";
            //sacamos los parametros
            string[] parametres = new string[separateCommand.Length-1];

            for (int i = 1; i < separateCommand.Length; i++)
			{
			    parametres[i-1] = separateCommand[i];
			}
            
            foreach (MethodInfo mi in methods)
            {
                //hay un metodo con el mismo numero de parametros
                if (mi.GetParameters().Length == separateCommand.Length - 1)
                {
                    object resultObj = Reflection.Invoke(mi, parametres);
                    if (resultObj == null)
                        result = "No devuelve nada";
                    else
                        result = resultObj.ToString();
                    break; //para no ejecutar mas de uno
                }
            }

            if (result == "null")
            {
                //hasta el momento no se ha encontrado ningun metodo
                result = "No se ha podido ejecutar ninguna función";
            }
            
            return result;
        }

        public bool ExitsCommand(string command)
        {
            String[] separateCommand = SeparateArguments(command);
            MethodInfo[] methods = Reflection.SearchMethod(separateCommand[0]);
            if (methods == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private String[] SeparateArguments(String command)
        {
            String[] result = command.Split(new char[] { ' ' });
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Replace("_", " ");
            }
            return result;
        }

        private String[] SeparateCommands(String str)
        {
            String[] result = str.Split(new char[] { '|' });
            return result;
        }

        //METHODS GUI
        public List<string> SearchCommands(string command)
        {
            MethodInfo[] methods = Reflection.GetAllMethods().Where(x => x.Name.ToLower().Contains(command.ToLower())).ToArray<MethodInfo>();
            List<string> result = new List<string>();
            if (methods.Length == 0)
            {
                result.Add("no hemos encontrado ningun metodo con texto de ese tipo");
            }
            else
            {
                foreach (MethodInfo method in methods)
                {
                    result.Add(InfoMethod(method));
                }
            }

            return result;
        }

        private string InfoMethod(MethodInfo method)
        {
            string result = method.Name;
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                result += " "+ClassParameter(parameter);
            }

            return result;
        }

        private string ClassParameter(ParameterInfo parameter)
        {
            string[] types = parameter.ParameterType.ToString().Split(new char[] { '.' });

            return "(" + types[types.Length - 1] + ")" + parameter.Name;
        }
    }
}
