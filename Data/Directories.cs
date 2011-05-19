using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Data
{
    public class Directories
    {
        public static string GetAlarmXML()
        {
            return Directory.GetCurrentDirectory() + "\\Data\\" + "Alarms.xml";
        }

        public static string GetHaarCascade()
        {
            return GetDataDirectory() + "\\HaarCascade\\haarcascade_frontalface.xml";
        }

        public static string GetImagesDirectory()
        {
            return Directory.GetCurrentDirectory() + "\\Images\\";
        }

        public static string GetPicturesDirectory()
        {
            return GetImagesDirectory() + "\\Pictures\\";
        }

        public static string GetFacesDirectory()
        {
            return GetImagesDirectory() + "\\Faces\\";
        }
        //TO MEJORAR (para evitar fallos con borrado)
        public static int GetNextNameFace()
        {
            FileInfo[] files = new DirectoryInfo(GetFacesDirectory()).GetFiles();
            return files.Length;
        }

        public static int GetNextNamePicture()
        {
            FileInfo[] files = new DirectoryInfo(GetPicturesDirectory()).GetFiles();
            return files.Length;
        }

        public static string GetDataDirectory()
        {
            return Directory.GetCurrentDirectory() + "\\Data\\";
        }

        public static string GetAuthorsXML()
        {
            return GetDataDirectory() + "Authors.xml";
        }

        public static string GetAlbumsXML()
        {
            return GetDataDirectory() + "Albums.xml";
        }

        public static string GetGenresXML()
        {
            return GetDataDirectory() + "Genres.xml";
        }

        public static string GetTitlesXML()
        {
            return GetDataDirectory() + "Titles.xml";
        }

        public static void DeleteMusicMediaXMLs()
        {
            File.Delete(GetTitlesXML());
            File.Delete(GetGenresXML());
            File.Delete(GetAlbumsXML());
            File.Delete(GetAuthorsXML());
        }

        public static string GetLanguageDirectory()
        {
            return Directory.GetCurrentDirectory() + "\\Language\\";
        }

        public static string GetLanguageXML()
        {
            return GetLanguageDirectory() + Languages.CodeRegion + ".xml";
        }

        public static string GetGrammarDirectory()
        {
            return Directory.GetCurrentDirectory() + "\\Grammar";
        }

        public static string GetGrammarXML()
        {
            return Directory.GetCurrentDirectory() + "\\Grammar\\" + Languages.CodeRegion + ".xml";
        }
    }
}
