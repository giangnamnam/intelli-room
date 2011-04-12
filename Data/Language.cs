using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Data
{
    public class Languages
    {
        private static string language;
        private static string codeRegion;
        private static string userAgent;

        public static string Language
        {
            get { return Languages.language; }
            set { Languages.language = value; }
        }

        public static string CodeRegion
        {
            get { return Languages.codeRegion; }
            set { Languages.codeRegion = value; }
        }

        public static string UserAgent
        {
            get { return Languages.userAgent; }
            set { Languages.userAgent = value; }
        }

        public static void LoadLanguaje()
        {
            //carga un lenguaje dado el codigo de Region
        }

        public static List<string> GetLanguages()
        {
            throw new NotImplementedException();
        }

        public static string GetText(String text)
        {
            //XmlDocument xml = new XmlDocument();
            //String directory = System.IO.Directory.GetCurrentDirectory()+"\\Language\\" + language + ".xml";
            //xml.Load(directory);
            //XmlElement elemnt=  xml.GetElementById("text");
            //return elemnt.InnerText;
            return text;
        }
    }
}