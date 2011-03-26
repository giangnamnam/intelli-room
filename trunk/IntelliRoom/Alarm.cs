using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace IntelliRoom
{
    class Alarms
    {
        List<Alarm> alarms;

        public Alarms()
        {
            this.alarms = new List<Alarm>();
            LoadAlarms();
        }

        public void LoadAlarms()
        {
            if (Directory.Exists(Directories.GetAlarmXML()))
            {
                XmlSerializer serialize = new XmlSerializer(this.alarms.GetType());
                XmlReader xml = XmlReader.Create(Directories.GetAlarmXML());
                if (serialize.CanDeserialize(xml))
                {
                    alarms = (List<Alarm>)serialize.Deserialize(xml);
                }
            }
        }
        
        public void SaveAlarms()
        {
            XmlSerializer serializer = new XmlSerializer(this.alarms.GetType());
            XmlWriter xml = XmlWriter.Create(Directories.GetAlarmXML());
            serializer.Serialize(xml, this.alarms);
        }

        public void AddAlarm(String name, DateTime date) 
        {
            alarms.Add(new Alarm(name, date));
        }

        public void AddAlarm(String name, int day, int month, int year, int hour, int minute)
        {
            alarms.Add(new Alarm(name, year, month, day, hour, minute));
        }
    }

    class Alarm
    {
        private String name;
        private DateTime date;

        public Alarm(String name, DateTime date)
        {
            this.name = name;
            this.date = date;
        }

        public Alarm(String name, int day, int month, int year, int hour, int minute)
        {
            this.name = name;
            this.date = new DateTime(year,month,day,hour,minute,0);
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
