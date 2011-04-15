using System;
using WMPLib;
using System.Collections.Generic;

namespace Media
{
    public class MediaPlayer : Media.IMediaPlayer
    {
        private WindowsMediaPlayer player;
        private MusicMedia media;

        public MediaPlayer()
        {
            player = new WindowsMediaPlayer();
            media = new MusicMedia(player);
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

        //MUSICMEDIA

        public List<String> GetAllAuthors()
        {
            return media.Authors;
        }

        public List<String> GetAllSong()
        {
            return media.Titles;
        }

        public List<String> GetAllGenre()
        {
            return media.Genres;
        }

        public List<String> GetAllAlbum()
        {
            return media.Albums;
        }

    }
}
