using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Data;
using WMPLib;

namespace Media
{
    public class MusicMedia
    {
        private List<string> authors;
        private List<string> genres;
        private List<string> albums;
        private List<string> titles;

        /// <summary>
        /// Contructor de MusicMedia
        /// </summary>
        /// <param name="playList">Le pasamos por parametros la PlayList que queremos transformar a MusicMedia</param>
        /// <param name="isMediaCollection">Le definimos si se trata de una MediaColleccion o de una PlayList</param>
        public MusicMedia(IWMPPlaylist playList, bool isMediaCollection)
        {
            authors = new List<string>();
            genres = new List<string>();
            albums = new List<string>();
            titles = new List<string>();

            if (isMediaCollection)
            {
                //lanzar hilo
                Thread thread = new Thread(new ParameterizedThreadStart(LoadMediaCollection));
                thread.Start(playList);
            }
            else
            {
                
            }
            
        }

        public List<string> Titles
        {
            get { return titles; }
        }

        public List<string> Albums
        {
            get { return albums; }
        }

        public List<string> Genres
        {
            get { return genres; }
        }

        public List<string> Authors
        {
            get { return authors; }
        }

        private void LoadMediaCollection(object playList)
        {

            if (IsPosibleDeserialize())
            {
                MediaDeserialize();
            }
            else
            {
                LoadPlayListCollection((IWMPPlaylist)playList);
            }

            //serializar
            MediaSerialize();
        }

        private static bool IsPosibleDeserialize()
        {
            return File.Exists(Directories.GetAuthorsXML()) && File.Exists(Directories.GetGenresXML()) && File.Exists(Directories.GetAlbumsXML()) && File.Exists(Directories.GetTitlesXML());
        }

        private void MediaDeserialize()
        {
            XmlSerializer serialize = new XmlSerializer(this.authors.GetType());
            XmlReader xml = XmlReader.Create(Directories.GetAuthorsXML());
            if (serialize.CanDeserialize(xml))
            {
                authors = (List<string>)serialize.Deserialize(xml);
            }

            serialize = new XmlSerializer(this.albums.GetType());
            xml = XmlReader.Create(Directories.GetAlbumsXML());
            if (serialize.CanDeserialize(xml))
            {
                albums = (List<string>)serialize.Deserialize(xml);
            }

            serialize = new XmlSerializer(this.titles.GetType());
            xml = XmlReader.Create(Directories.GetTitlesXML());
            if (serialize.CanDeserialize(xml))
            {
                titles = (List<string>)serialize.Deserialize(xml);
            }

            serialize = new XmlSerializer(this.genres.GetType());
            xml = XmlReader.Create(Directories.GetGenresXML());
            if (serialize.CanDeserialize(xml))
            {
                genres = (List<string>)serialize.Deserialize(xml);
            }
        }

        private void LoadPlayListCollection(IWMPPlaylist playList)
        {
            List<string> authors = new List<string>();
            List<string> genres = new List<string>();
            List<string> albums = new List<string>();
            List<string> titles = new List<string>();

            for (int i = 0; i < playList.count; i++)
            {
                if (AddElementInMedia(authors,playList.Item[i].getItemInfo("Author").ToString()))
                {
                    authors.Add(playList.Item[i].getItemInfo("Author").ToString());
                }

                if (AddElementInMedia(genres,playList.Item[i].getItemInfo("Genre").ToString()))
                {
                    genres.Add(playList.Item[i].getItemInfo("Genre").ToString());
                }

                if (AddElementInMedia(albums, playList.Item[i].getItemInfo("Album").ToString()))
                {
                    albums.Add(playList.Item[i].getItemInfo("Album").ToString());
                }

                if (AddElementInMedia(titles, playList.Item[i].getItemInfo("Title").ToString()))
                {
                    titles.Add(playList.Item[i].getItemInfo("Title").ToString());
                }
            }

            //asignar a la clase
            this.albums = albums;
            this.authors = authors;
            this.genres = genres;
            this.titles = titles;

            
        }

        private void MediaSerialize()
        {
            XmlSerializer serializer = new XmlSerializer(this.albums.GetType());
            XmlWriter xml = XmlWriter.Create(Directories.GetAlbumsXML());
            serializer.Serialize(xml, this.albums);
            xml.Close();

            serializer = new XmlSerializer(this.authors.GetType());
            xml = XmlWriter.Create(Directories.GetAuthorsXML());
            serializer.Serialize(xml, this.authors);

            serializer = new XmlSerializer(this.genres.GetType());
            xml = XmlWriter.Create(Directories.GetGenresXML());
            serializer.Serialize(xml, this.genres);

            serializer = new XmlSerializer(this.titles.GetType());
            xml = XmlWriter.Create(Directories.GetTitlesXML());
            serializer.Serialize(xml, this.titles);
        }

        private bool AddElementInMedia(List<string> list, string element)
        {
            bool result = true;
            if (element == "")
            {
                result = false;
            }
            if (result)
            {
                string elem = element.ToLower();
                bool exist = list.Exists(x => x.ToLower() == elem);
                result = !exist;
            }
            return result;
        }

        public override string ToString()
        {
            string result = "----MEDIA----\n";
            result += "---------" + authors.Count + " Authors---------\n";
            foreach (string element in authors)
	        {
                result += element + "\n";
	        }
            result += "\n";
            result += "---------" + genres.Count + " Genres---------\n";
            foreach (string element in genres)
            {
                result += element + "\n";
            }
            result += "\n";
            result += "---------" + albums.Count + " Albums---------\n";
            foreach (string element in albums)
            {
                result += element + "\n";
            }
            result += "\n";
            result += "---------" + titles.Count + " Titles---------\n";
            foreach (string element in titles)
            {
                result += element + "\n";
            }

            return result;            
        }
    }
}
