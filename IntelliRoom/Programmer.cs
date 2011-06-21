using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Data;
using System.Globalization;

namespace IntelliRoom
{
    class Programmer
    {
        List<Task> tasks;

        public Programmer()
        {
            this.tasks = new List<Task>();
            LoadTasks();
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
    }

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
            this.date = new DateTime(year,month,day,hour,minute,0);
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
    }
}
