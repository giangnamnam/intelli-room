using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace TextXML
{
    class Program
    {
        static void Main(string[] args)
        {
            
            XmlDocument xml = new XmlDocument();
            String directory = System.IO.Directory.GetCurrentDirectory() + "\\English.xml";
            xml.Load(directory);
            XmlElement elemnt = xml.GetElementById(Console.ReadLine());
            Console.WriteLine(elemnt.InnerText);
        }
    }
}
