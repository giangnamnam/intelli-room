using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Data;

namespace IntelliRoom
{
    class SystemConfiguration
    {
        List<Configuration> configurations;

        public SystemConfiguration()
        {
            this.configurations = new List<Configuration>();
            LoadConfigurations();
        }

        public void ExecuteConfiguration(String name)
        {
            //buscamos comandos dentro de configuraciones que hayan cumplido
            List<Configuration> toConfiguration = configurations.Where(x => x.Name == name).ToList();

            //ejecutamos todas las que van a ejecutarse
            foreach (Configuration config in toConfiguration)
            {
                config.ExecuteConfiguration();
            }
        }

        public void LoadConfigurations()
        {
            if (Directory.Exists(Directories.GetTasksXML()))
            {
                XmlSerializer serialize = new XmlSerializer(this.configurations.GetType());
                XmlReader xml = XmlReader.Create(Directories.GetConfigurationXML());
                if (serialize.CanDeserialize(xml))
                {
                    configurations = (List<Configuration>)serialize.Deserialize(xml);
                }
            }
        }
        
        public void SaveConfigurations()
        {
            XmlSerializer serializer = new XmlSerializer(this.configurations.GetType());
            XmlWriter xml = XmlWriter.Create(Directories.GetConfigurationXML());
            serializer.Serialize(xml, this.configurations);
        }

        public void AddConfiguration(string name, string command) 
        {
            configurations.Add(new Configuration(name, command));
        }

        public void DeleteConfiguration()
        {
            configurations.Clear();
        }

        public List<string> GetConfigurations()
        {
            List<string> configs = new List<string>();
            foreach (Configuration config in configurations)
            {
                if (!configs.Contains(config.Name)) configs.Add(config.Name);
            }
            return configs;
        }

        class Configuration
        {
            private String name;
            private String command;

            public Configuration(String name, String command)
            {
                this.command = command;
                this.name = name;
            }

            public String Name
            {
                get { return name; }
                set { name = value; }
            }
            
            public String Command
            {
                get { return command; }
                set { command = value; }
            }

            public void ExecuteConfiguration()
            {
                string[] commands = command.Split('|');
                foreach (string cmd in commands)
                {
                    Execute(cmd);
                }
            }

            private void Execute(string command)
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
                            Data.InfoMessages.InformationMessage("Se ejecutó el comando " + command + " desde el sistema de configuración");
                            break; //para no ejecutar mas de uno
                        }
                    }
                }
                //return result;
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
        }
    }
}
