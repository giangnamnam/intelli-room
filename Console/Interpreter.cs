using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using IntelliRoom;

namespace Console
{
    public class CommandInterpreter
    {
        //TODO -> colores        
        public String Interpreter(String toInterpreter)
        {
            toInterpreter = toInterpreter.ToLower();
            String result = "";
            if (toInterpreter != null && toInterpreter!="" && toInterpreter[0] == '?')
            {
                toInterpreter = toInterpreter.TrimStart(new char[]{'?'});
                result = CommandHelp(toInterpreter);
            }
            else if (toInterpreter != null && toInterpreter != "" && toInterpreter == "allcommands")
            {
                result = AllCommands();
            }
            else if (toInterpreter != null && toInterpreter != "" && toInterpreter == "help")
            {
                result = Help();
            }
            else if (toInterpreter != null && toInterpreter != "" && SeparateArguments(toInterpreter)[0] == "searchcommand")
            {
                if (SeparateArguments(toInterpreter).Length != 1)
                {
                    result = SearchCommand(SeparateArguments(toInterpreter)[1]);
                }
                else
                {
                    result = "Siga la estructura siguiente para la busqueda: SearchComand textoABuscar";
                }
            }
            else
            {
                string[] commands = SeparateCommands(toInterpreter);
                foreach (string command in commands)
                {
                    result += Command(command) + "\n";
                }
            }

            return result;
        }

        private String Command(String command)
        {
            String[] separateCommand = SeparateArguments(command);
            MethodInfo[] methods = Reflection.SearchMethod(separateCommand[0]);
            String result = "";
            //sacamos los parametros
            string[] parametres = new string[separateCommand.Length-1];

            for (int i = 1; i < separateCommand.Length; i++)
			{
			    parametres[i-1] = separateCommand[i];
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
                        if (resultObj == null)
                            result = "No devuelve nada";
                        else
                            result = resultObj.ToString();
                        break; //para no ejecutar mas de uno
                    }
                }

                if (result == "")
                {
                    //hasta el momento no se ha encontrado ningun metodo
                    result = PrintInfoMethods(methods);
                }
            }
            else
            {
                result = "no existe ningun comando con ese nombre, si no sabes utilizar el programa, pueba a utilizar el comando <Help>";
            }
            return result;
        }

        private String Help()
        {
            String help = "Escribe comandos con la siguiente configuracion:\n";
            help += "<nombrecomando> <primer_argumento> <segundo_argumento> ... <enesimo_argumento>\n\n";
            help += "Si no conoces los comandos puedes utilizar los comandos de ayuda:\n";
            help += "<AllCommands> -> devuelve una lista con todos los comandos disponibles \n";
            help += "<SearchCommand> <cadena> -> hace una busqueda de comandos donde encaje la cadena";

            return help;
        }

        private String CommandHelp(String command)
        {
            String[] separateCommand = SeparateArguments(command);
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
                //hasta el momento no se ha encontrado ningun metodo
                result = PrintInfoMethods(methods);
            }
            else
            {
                result = "no existe ningun comando con ese nombre";
            }
            return result;
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

        public String AllCommands()
        {
            return PrintInfoMethods(Reflection.GetMethods());
        }

        public String SearchCommand(string name)
        {
            MethodInfo[] methods = Reflection.GetMethods().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToArray<MethodInfo>();
            string result = "";
            if (methods.Length == 0)
            {
                result = "no hemos encontrado ningun metodo con texto de ese tipo";
            }
            else
            {
                result = PrintInfoMethods(methods);
            }
            return result;
        }

        public static String PrintInfoMethods(MethodInfo[] methods)
        {
            methods.OrderBy(x => x.Name);
            List<string> nameMethods = new List<string>();
            string result = "";
            int i = 1;
            foreach (MethodInfo method in methods)
            {
                if (!nameMethods.Contains(method.Name))
                {
                    nameMethods.Add(method.Name);
                    i = 1;
                    result += "Metodo " + method.Name + ": \n";
                    result += i.ToString() + " - " + PrintInfoOverride(method) + "\n";
                }
                else
                {
                    result += i.ToString() + " - " + PrintInfoOverride(method) + "\n";
                }
                i++;
            }

            return result;
        }

        private static String PrintInfoOverride(MethodInfo method)
        {
            ParameterInfo[] paramenters = method.GetParameters();
            String res = "";
            if (paramenters.Length != 0)
            {
                res += "Con " + paramenters.Length + " parametros, del tipo: ";
                foreach (ParameterInfo pi in paramenters)
                {
                    res += pi.ParameterType.ToString() + ", ";
                }
            }
            else
            {
                res += "Sin parametros de entrada";
            }
            return res;
        }
    }
}
