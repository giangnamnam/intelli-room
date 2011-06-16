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
            try
            {
                return player.controls.currentItem.getItemInfo("Author");
            }
            catch (Exception)
            {
                return "Ningún autor en reproducción";
            }
            
        }

        public string GetInfoGenre()
        {
            try
            {
                return player.controls.currentItem.getItemInfo("Genre");
            }
            catch (Exception)
            {
                return "Ningún género en reproducción";
            }
        }

        public string GetInfoAlbum()
        {
            try
            {
                return player.controls.currentItem.getItemInfo("Album");
            }
            catch (Exception)
            {
                return "Ningún álbum en reproducción";
            }
        }
        
        public string GetInfoTitle()
        {
            try
            {
                return player.controls.currentItem.getItemInfo("Title");
            }
            catch (Exception)
            {
                return "Ningún título en reproducción";
            }
        }

        public string GetInfoDuration()
        {
            try
            {
                return player.controls.currentItem.durationString;
            }
            catch (Exception)
            {
                return "Ninguna reproducción en curso";
            }
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

        public void UpdateMusicMedia()
        {
            //eliminamos los archivos serializados del directorio
            Data.Directories.DeleteMusicMediaXMLs();
            media = new MusicMedia(player.mediaCollection.getAll(), true);
        }

        //PLAY SOUND

        public void PlaySound(String urlSound)
        {
            PlaySounds sounds = new PlaySounds(urlSound);
            sounds.finishSound += new EventHandler(sounds_finishSound);
            Pause();
        }

        void sounds_finishSound(object sender, EventArgs e)
        {
            Play();
        }

    }
}
