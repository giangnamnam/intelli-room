using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Message
    {
        static List<string> InfoMessages = new List<string>();
        static List<string> ErrorMessages = new List<string>();

        public static void InformationMessage(string information)
        {
            InfoMessages.Add(information);
            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Info: " + information);
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ErrorMessage(string error)
        {
            ErrorMessages.Add(error);
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Error: " + error);
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowInformationMessage(int numMessages)
        {
            for (int i = numMessages; i < InfoMessages.Count; i++)
            {
                InformationMessage(InfoMessages[i]);
            }
        }

        public static void ShowErrorMessage(int numMessages)
        {
            for (int i = numMessages; i < InfoMessages.Count; i++)
            {
                ErrorMessage(ErrorMessages[i]);
            }
        }

    }
}
