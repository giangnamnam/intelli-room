using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Speech.Recognition;

namespace Test3
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateGrammar.LoadGrammar();
        }
    }

    class GenerateGrammar
    {
        public static void LoadGrammar()
        {
            
            //cargamos el documento XML
            XmlDocument xml = new XmlDocument();
            xml.Load("./Spanish.xml");

            foreach (XmlNode command in xml.ChildNodes[1].ChildNodes)
            {
                LoadCommand(command);
            }
        }

        private static Grammar LoadCommand (XmlNode commandNode)
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            

            foreach (XmlNode choice in commandNode.ChildNodes)
            {
                grammarBuilder.Append(LoadChoices(choice));
            }

            Grammar command = new Grammar(grammarBuilder);
            //introduzco nombre de la gramatica
            command.Name = commandNode.Attributes[0].Value.ToString();

            return command;
            
        }

        private static Choices LoadChoices(XmlNode choiceNode)
        {
            Choices choices = new Choices();

            foreach (XmlNode element in choiceNode.ChildNodes)
            {
                if (element.FirstChild == null)
                {
                    choices.Add(" ");
                }
                else
                {
                    choices.Add(element.FirstChild.InnerText);
                }
                
            }

            return choices;
        }

    }
}
