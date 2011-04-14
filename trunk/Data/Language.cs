using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Data
{
    public class Languages
    {
        private static string language = "Spanish";
        private static string codeRegion = "es";
        private static string userAgent = "Mozilla/5.0 (X11; U; Linux i686; es-ES; rv:1.9.0.2) Gecko/2008092313 Ubuntu/9.25 (jaunty) Firefox/3.8";

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
            //carga un lenguaje dado el codigo de Region TODO

            language = "Spanish";
            codeRegion = "es";
            userAgent = "Mozilla/5.0 (X11; U; Linux i686; es-ES; rv:1.9.0.2) Gecko/2008092313 Ubuntu/9.25 (jaunty) Firefox/3.8";

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