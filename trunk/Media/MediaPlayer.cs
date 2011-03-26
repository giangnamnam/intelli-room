using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using WMPLib;

namespace Media
{
    public class MediaPlayer : Media.IMediaPlayer
    {
        private WindowsMediaPlayer player;
        private List<String> artists;
        private List<String> generes;
        private List<String> albums;
        private List<String> songs;

        public MediaPlayer()
        {
            player = new WindowsMediaPlayer();
            //Thread hilo = new Thread(GetAllInfoMedia());
        }

        public void Play()
        {
            player.controls.play();
        }

        public void Pause()
        {
            player.controls.pause();
        }

        public void Stop()
        {
            player.controls.stop();
        }

        public void Forward()
        {
            player.controls.next();
        }

        public void Rewind()
        {
            player.controls.previous();
        }

        public int IncreaseVolume()
        {
            player.settings.volume = player.settings.volume + 20;
            return player.settings.volume;
        }

        public int DecreaseVolume()
        {
            player.settings.volume = player.settings.volume - 20;
            return player.settings.volume;
        }

        public void ChangeVolume(int newVolume)
        {
            player.settings.volume = newVolume;
        }

        public void Mute()
        {
            player.settings.volume = 0;
        }

        public void Shuffle()
        {
            throw new NotImplementedException();
        }

        public void RepeatAll()
        {
            throw new NotImplementedException();
        }

        public void RepeatOne()
        {
            throw new NotImplementedException();
        }

        public void NotRepeat()
        {
            throw new NotImplementedException();
        }

        //STRING

        public string GetInfoArtist()
        {
            throw new NotImplementedException();
        }

        public string GetInfoDuration()
        {
            return player.controls.currentItem.durationString;
        }

        public string GetInfoDisc()
        {
            throw new NotImplementedException();
        }

        public string GetInfoTitle()
        {
            return player.controls.currentItem.name;
        }

        public string GetInfoPlayList()
        {
            string result = "";
            
            for (int i = 0; i < player.currentPlaylist.count; i++)
            {
                result += player.currentPlaylist.Item[i].name + "\n";
            }

            return result;
        }
    
        //MEDIA COLLECTION

        public void LoadMediaArtist(string nameArtist)
        {
            player.currentPlaylist = player.mediaCollection.getByAuthor(nameArtist);
        }

        public void LoadMediaSong(string nameSong)
        {
            player.currentPlaylist = player.mediaCollection.getByName(nameSong);
        }

        public void LoadMediaGenre(string nameGenre)
        {
            player.currentPlaylist = player.mediaCollection.getByGenre(nameGenre);
        }

        public void LoadMediaAlbum(string nameAlbum)
        {
            player.currentPlaylist = player.mediaCollection.getByAlbum(nameAlbum);
        }

        public void LoadAllMedia() 
        {
            player.currentPlaylist = player.mediaCollection.getAll();
        }

        public void LoadUrl(string url) 
        {
            player.URL = url;
        }


        //INFO MEDIA COLLECTION
        public List<String> GetAllArtist()
        {
            return artists;
        }

        public List<String> GetAllSong()
        {
            return songs;
        }

        public List<String> GetAllGenere()
        {
            return generes;
        }

        public List<String> GetAllAlbum()
        {
            return albums;
        }

        private void GetAllInfoMedia()
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

            //asignar a la clase
            this.albums = albums;
            this.artists = authors;
            this.generes = genres;
            this.songs = songs;

        }
    }
}
