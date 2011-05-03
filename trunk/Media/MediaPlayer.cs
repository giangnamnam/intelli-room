using System;
using WMPLib;
using System.Collections.Generic;

namespace Media
{
    public class MediaPlayer: IMediaPlayer
    {
        private WindowsMediaPlayer player;
        private MusicMedia media;

        public MediaPlayer()
        {
            player = new WindowsMediaPlayer();
            media = new MusicMedia(player.mediaCollection.getAll(),true);
        }

        public MusicMedia Media
        {
            get { return media; }
        }

        //CONTROL

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

        public void IncreaseVolume()
        {
            player.settings.volume = player.settings.volume + 20;
        }

        public void DecreaseVolume()
        {
            player.settings.volume = player.settings.volume - 20;
        }

        public void ChangeVolume(int newVolume)
        {
            player.settings.volume = newVolume;
        }

        public int GetVolume()
        {
            return player.settings.volume;
        }

        public void Mute()
        {
            player.settings.volume = 0;
        }

        //INFO

        public string GetInfoAuthor()
        {
            return player.controls.currentItem.getItemInfo("Author");
        }

        public string GetInfoGenre()
        {
            return player.controls.currentItem.getItemInfo("Genre");
        }

        public string GetInfoAlbum()
        {
            return player.controls.currentItem.getItemInfo("Album");
        }
        
        public string GetInfoTitle()
        {
            return player.controls.currentItem.getItemInfo("Title");
        }

        public string GetInfoDuration()
        {
            return player.controls.currentItem.durationString;
        }

        public MusicMedia GetInfoPlayList()
        {
            return new MusicMedia(player.currentPlaylist,false);
        }

        public MusicMedia GetInfoMedia()
        {
            return Media;
        }

    
        //MEDIA COLLECTION

        public void LoadMediaAuthor(string nameAuthor)
        {
            player.currentPlaylist = player.mediaCollection.getByAuthor(nameAuthor);
        }

        public void LoadMediaTitle(string nameTitle)
        {
            player.currentPlaylist = player.mediaCollection.getByName(nameTitle);
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
            return Media.Authors;
        }

        public List<String> GetAllTitles()
        {
            return Media.Titles;
        }

        public List<String> GetAllGenres()
        {
            return Media.Genres;
        }

        public List<String> GetAllAlbums()
        {
            return Media.Albums;
        }

    }
}
