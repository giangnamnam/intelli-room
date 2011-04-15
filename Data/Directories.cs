using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Data
{
    public class Directories
    {
        //TODO: METODO PARA CREAR DIRECTORIOS POR SI NO ESTAN CREADOS
        public static string GetAlarmXML()
        {
            return Directory.GetCurrentDirectory() + "\\Data\\" + "Alarms.xml";
        }

        public static string GetMediaDirectory()
        {
            return Directory.GetCurrentDirectory() + "\\Data\\Media\\";
        }

        public static string GetAuthorsXML()
        {
            return GetMediaDirectory() + "Authors.xml";
        }

        public static string GetAlbumsXML()
        {
            return GetMediaDirectory() + "Albums.xml";
        }

        public static string GetGenresXML()
        {
            return GetMediaDirectory() + "Genres.xml";
        }

        public static string GetTitlesXML()
        {
            return GetMediaDirectory() + "Titles.xml";
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
