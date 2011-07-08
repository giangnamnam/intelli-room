using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Data;
using System.Globalization;
using System.Reflection;
using System.Timers;
using System.Linq;

namespace IntelliRoom
{
    class Programmer
    {
        List<Task> tasks;
        Timer timer;

        public Programmer()
        {
            this.tasks = new List<Task>();
            LoadTasks();
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(CkeckTasks);
            timer.Interval = 1000 * 30; //cada 30 segundos hacemos una comprobacion
            timer.Enabled = true;
            GC.KeepAlive(timer);
        }

        void CkeckTasks(object sender, ElapsedEventArgs e)
        {
            //buscamos tareas que hayan cumplido
            DateTime now = DateTime.Now;
            List<Task> toExecute = tasks.Where(x => (x.Date.CompareTo(now) <= 0)).ToList();
            //las eliminamos de la lista de tareas
            foreach (Task task in toExecute)
            {
                tasks.Remove(task);
            }
            //ejecutamos todas las que van a ejecutarse
            foreach (Task task in toExecute)
            {
                task.ExecuteTask();
            }
        }

        public void LoadTasks()
        {
            if (Directory.Exists(Directories.GetTasksXML()))
            {
                XmlSerializer serialize = new XmlSerializer(this.tasks.GetType());
                XmlReader xml = XmlReader.Create(Directories.GetTasksXML());
                if (serialize.CanDeserialize(xml))
                {
                    tasks = (List<Task>)serialize.Deserialize(xml);
                }
            }
        }
        
        public void SaveTasks()
        {
            XmlSerializer serializer = new XmlSerializer(this.tasks.GetType());
            XmlWriter xml = XmlWriter.Create(Directories.GetTasksXML());
            serializer.Serialize(xml, this.tasks);
        }

        public void AddTask(string command, DateTime date) 
        {
            tasks.Add(new Task(command, date));
        }

        public void AddTask(string command, int day, int month, int year, int hour, int minute)
        {
            tasks.Add(new Task(command, year, month, day, hour, minute));
        }

        public void AddTask(string command, int hour, int minute)
        {
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            tasks.Add(new Task(command, day, month, year, hour, minute));
        }

        public ElapsedEventHandler ckeckTasks { get; set; }


        class Task
        {
            private String command;
            private DateTime date;

            public Task(String command, DateTime date)
            {
                this.command = command;
                this.date = date;
            }

            public Task(String command, int day, int month, int year, int hour, int minute)
            {
                this.command = command;
                this.date = new DateTime(year, month, day, hour, minute, 0);
            }

            public DateTime Date
            {
                get { return date; }
                set { date = value; }
            }

            public String Command
            {
                get { return command; }
                set { command = value; }
            }

            public void ExecuteTask()
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
                            Data.InfoMessages.InformationMessage("Se ejecuto el comando " + command + " desde el programador de tareas");
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
