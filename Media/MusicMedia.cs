using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Data;
using WMPLib;

namespace Media
{
    class MusicMedia
    {
        private List<string> authors;
        private List<string> genres;
        private List<string> albums;
        private List<string> titles;

        public MusicMedia(WindowsMediaPlayer player)
        {
            authors = new List<string>();
            genres = new List<string>();
            albums = new List<string>();
            titles = new List<string>();

            LoadMedia(player);
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

        private void LoadMedia(WindowsMediaPlayer player)
        {

            if (File.Exists(Directories.GetAuthorsXML()) && File.Exists(Directories.GetGenresXML()) && File.Exists(Directories.GetAlbumsXML()) && File.Exists(Directories.GetTitlesXML()))
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
            else
            {
                //lanzar hilo
                Thread thread = new Thread(new ParameterizedThreadStart(UpdateAllInfoMedia));
                thread.Start(player);
            }
        }

        private void UpdateAllInfoMedia(object player)
        {
            
            //cargar la lista
            IWMPPlaylist media = ((WindowsMediaPlayer)player).mediaCollection.getAll();
            List<string> authors = new List<string>();
            List<string> genres = new List<string>();
            List<string> albums = new List<string>();
            List<string> titles = new List<string>();
            //TODO: mirar si esos son los atributos
            for (int i = 0; i < media.count; i++)
            {
                if (AddElementInMedia(authors,media.Item[i].getItemInfo("Author").ToString()))
                {
                    authors.Add(media.Item[i].getItemInfo("Author").ToString());
                }

                if (AddElementInMedia(genres,media.Item[i].getItemInfo("Genre").ToString()))
                {
                    genres.Add(media.Item[i].getItemInfo("Genre").ToString());
                }

                if (AddElementInMedia(albums, media.Item[i].getItemInfo("Album").ToString()))
                {
                    albums.Add(media.Item[i].getItemInfo("Album").ToString());
                }

                if (AddElementInMedia(titles, media.Item[i].getItemInfo("Title").ToString()))
                {
                    titles.Add(media.Item[i].getItemInfo("Title").ToString());
                }
            }

            //asignar a la clase
            this.albums = albums;
            this.authors = authors;
            this.genres = genres;
            this.titles = titles;

            //serializar
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

        public bool AddElementInMedia(List<string> list, string element)
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
    }
}
