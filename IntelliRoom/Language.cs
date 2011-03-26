using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IntelliRoom
{
    public class Language
    {
        String language;
        public Language(String languaje)
        {
            this.language=languaje;
        }
        public String GetLanguaje()
        {
            return this.language;
        }
        public String GetText(String text)
        {
            //XmlDocument xml = new XmlDocument();
            //String directory = System.IO.Directory.GetCurrentDirectory()+"\\Language\\" + language + ".xml";
            //xml.Load(directory);
            //XmlElement elemnt=  xml.GetElementById("text");
            //return elemnt.InnerText;
            return text;
        }
    }

    public class Spanish : Language
    {
        public Spanish():base("Spanish"){ }
    }

    public class English : Language
    {
        public English() : base("English") { }
    }
}
