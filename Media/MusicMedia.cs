using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;
using System.Xml.Serialization;
using Data;
using System.Xml;
using System.Threading;
using System.IO;

namespace Media
{
    class MusicMedia
    {
        private List<string> authors;
        private List<string> genres;
        private List<string> albums;
        private List<string> songs;

        public MusicMedia(WindowsMediaPlayer player)
        {
            authors = new List<string>();
            genres = new List<string>();
            albums = new List<string>();
            songs = new List<string>();

            LoadMedia(player);
        }

        public List<string> Songs
        {
            get { return songs; }
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
            if (Directory.Exists(Directories.GetAuthorsXML()) && Directory.Exists(Directories.GetGenresXML()) && Directory.Exists(Directories.GetAlbumsXML()) && Directory.Exists(Directories.GetSongsXML()))
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

                serialize = new XmlSerializer(this.songs.GetType());
                xml = XmlReader.Create(Directories.GetSongsXML());
                if (serialize.CanDeserialize(xml))
                {
                    songs = (List<string>)serialize.Deserialize(xml);
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
                //Thread thread = new Thread(new ParameterizedThreadStart(UpdateAllInfoMedia(player)));
                //thread.Start(player);
                //thread.


                UpdateAllInfoMedia(player);
            }
        }

        private void UpdateAllInfoMedia(WindowsMediaPlayer player)
        {

            //cargar la lista
            IWMPPlaylist media = player.mediaCollection.getAll();
            List<string> authors = new List<string>();
            List<string> genres = new List<string>();
            List<string> albums = new List<string>();
            List<string> songs = new List<string>();
            //TODO: mirar si esos son los atributos
            for (int i = 0; i < media.count; i++)
            {
                if (!authors.Contains(media.Item[i].getItemInfo("Author").ToString()))
                {
                    authors.Add(media.Item[i].getItemInfo("Author").ToString());
                }

                if (!genres.Contains(media.Item[i].getItemInfo("Genere").ToString()))
                {
                    genres.Add(media.Item[i].getItemInfo("Genere").ToString());
                }

                if (!albums.Contains(media.Item[i].getItemInfo("Album").ToString()))
                {
                    albums.Add(media.Item[i].getItemInfo("Album").ToString());
                }

                if (!songs.Contains(media.Item[i].getItemInfo("Song").ToString()))
                {
                    songs.Add(media.Item[i].getItemInfo("Song").ToString());
                }
            }
            //serializar

            XmlSerializer serializer = new XmlSerializer(this.albums.GetType());
            XmlWriter xml = XmlWriter.Create(Directories.GetAlbumsXML());
            serializer.Serialize(xml, this.albums);

            serializer = new XmlSerializer(this.authors.GetType());
            xml = XmlWriter.Create(Directories.GetAuthorsXML());
            serializer.Serialize(xml, this.authors);

            serializer = new XmlSerializer(this.genres.GetType());
            xml = XmlWriter.Create(Directories.GetGenresXML());
            serializer.Serialize(xml, this.genres);

            serializer = new XmlSerializer(this.songs.GetType());
            xml = XmlWriter.Create(Directories.GetSongsXML());
            serializer.Serialize(xml, this.songs);

            //asignar a la clase
            this.albums = albums;
            this.authors = authors;
            this.genres = genres;
            this.songs = songs;

        }
    }
}
