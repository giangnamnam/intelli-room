using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class InfoMessages
    {
        public static event EventHandler<Message> newMessage;
        private static List<Message> messages = new List<Message>();

        public static List<Message> Messages
        {
            get { return InfoMessages.messages; }
            set { InfoMessages.messages = value; }
        }

        public static void InformationMessage(string information)
        {
            messages.Insert(0, new Message("Info", information));
            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Info: " + information);
            System.Console.ForegroundColor = ConsoleColor.White;
            newMessage.Invoke(null, new Message("Info", information));
        }

        public static void ErrorMessage(string error)
        {
            messages.Insert(0, new Message("Error", error));
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Error: " + error);
            System.Console.ForegroundColor = ConsoleColor.White;
            newMessage.Invoke(null, new Message("Error", error));
        }

        public static void ShowInformationMessage(int numMessages)
        {
            for (int i = numMessages; i < messages.Count; i++)
            {
                InformationMessage(messages[i].ToString());
            }
        }

        public static void ShowErrorMessage(int numMessages)
        {
            for (int i = numMessages; i < messages.Count; i++)
            {
                ErrorMessage(Messages[i].ToString());
            }
        }

        public static List<string> GetAllTextMessages()
        {
            List<string> result = new List<string>();
            
            foreach (Message msg in messages)
            {
                result.Add(msg.ToString());
            }
            return result;
        }

        public class Message : IComparable<Message>
        {
            private DateTime date;
            private string text;
            private string type;

            public string Type
            {
                get { return type; }
                set { type = value; }
            }

            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            public DateTime Date
            {
                get { return date; }
                set { date = value; }
            }

            public Message(string type, string text)
            {
                date = DateTime.Now;
                this.text = text;
                this.type = type;
            }

            public int CompareTo(Message other)
            {
                return DateTime.Compare(this.date, other.date);
            }

            public override string ToString()
            {
                return type + " - " + date.ToString() + ": " + text;
            }
        }
    }
}
