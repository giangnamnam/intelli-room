using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Data
{
    public class Directories
    {
        public static string dir = Directory.GetCurrentDirectory() + "\\Grammar\\" + Languages.CodeRegion + ".xml";

        public static string GetAlarmXML()
        {
            return Directory.GetCurrentDirectory() + "\\Data\\" + "Alarms.xml";
        }

        public static string GetDirectoryLanguage()
        {
            return Directory.GetCurrentDirectory() + "\\Language";
        }

        public static string GetLanguageXML()
        {
            return Directory.GetCurrentDirectory() + "\\Language\\" + Languages.CodeRegion + ".xml";
        }

        public static string GetDirectoryGrammar()
        {
            return Directory.GetCurrentDirectory() + "\\Grammar";
        }

        public static string GetGrammarXML()
        {
            return Directory.GetCurrentDirectory() + "\\Grammar\\" + Languages.CodeRegion + ".xml";
        }
    }
}
