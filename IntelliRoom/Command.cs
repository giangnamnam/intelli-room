using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Media;

namespace IntelliRoom
{
    public class Command
    {
        //VOICE
        public void Speak(String speakText)
        {
            IntelliRoomSystem.voiceEngine.Speak(speakText);
        }

        public void DeleteAllGrammars()
        {
            IntelliRoomSystem.voiceEngine.DeleteAllGrammars();
        }

        public void DictationMode()
        {
            IntelliRoomSystem.voiceEngine.DictationMode();
        }

        public void ChangePrecisionRecognizer(int precision)
        {
            IntelliRoomSystem.voiceEngine.ChangePrecisionRecognizer(precision);
        }

        public void AddGrammar(Grammar grammar)
        {
            IntelliRoomSystem.voiceEngine.AddGrammar(grammar);
        }

        //UTILS
        public string GetDate()
        {
            return Utils.GetDate();
        }

        public string GetTime()
        {
            return Utils.GetTime();
        }

        //public string GetWeather(string city)
        //{
        //    return Utils.GetWeather(city);
        //}

        //LIGHTING

        public void SetDirectColor(String color)
        {
            IntelliRoomSystem.lighting.SetDirectColor(color);
        }


        //MEDIA
        public void Play() 
        {
            IntelliRoomSystem.media.Play();
        }

        public void Pause()
        {
            IntelliRoomSystem.media.Pause();
        }

        public void Forward()
        {
            IntelliRoomSystem.media.Forward();
        }

        public void ChangeVolume(int i)
        {
            IntelliRoomSystem.media.ChangeVolume(i);
        }

        public int DecreaseVolume()
        {
            return IntelliRoomSystem.media.DecreaseVolume();
        }

        public string GetInfoArtist()
        {
            return IntelliRoomSystem.media.GetInfoArtist();
        }

        public string GetInfoDisc()
        {
            return IntelliRoomSystem.media.GetInfoDisc();
        }

        public string GetInfoDuration()
        {
            return IntelliRoomSystem.media.GetInfoDuration();
        }

        public string GetInfoPlayList()
        {
            return IntelliRoomSystem.media.GetInfoPlayList();
        }

        public string GetInfoTitle()
        {
            return IntelliRoomSystem.media.GetInfoTitle();
        }

        public int IncreaseVolume()
        {
            return IntelliRoomSystem.media.IncreaseVolume();
        }

        public void MinimumVolume()
        {
            ChangeVolume(1);
        }

        public void MaximumVolume()
        {
            ChangeVolume(100);
        }

        public void LoadAllMedia()
        {
            IntelliRoomSystem.media.LoadAllMedia();
        }

        public void LoadAlbum(string nameAlbum)
        {
            IntelliRoomSystem.media.LoadMediaAlbum(nameAlbum);
        }

        public void LoadArtist(string nameArtist)
        {
            IntelliRoomSystem.media.LoadMediaArtist(nameArtist);
        }

        public void LoadGenere(string nameGenre)
        {
            IntelliRoomSystem.media.LoadMediaGenre(nameGenre);
        }

        public void LoadSong(string nameSong)
        {
            IntelliRoomSystem.media.LoadMediaSong(nameSong);
        }

        public void LoadUrl(string url)
        {
            IntelliRoomSystem.media.LoadUrl(url);
        }
        public void Mute()
        {
            IntelliRoomSystem.media.Mute();
        }

        public void NotRepeat()
        {
            IntelliRoomSystem.media.NotRepeat();
        }

        public void RepeatAll()
        {
            IntelliRoomSystem.media.RepeatAll();
        }

        public void RepeatOne()
        {
            IntelliRoomSystem.media.RepeatOne();
        }

        public void Rewind()
        {
            IntelliRoomSystem.media.Rewind();
        }
        public void Shuffle()
        {
            IntelliRoomSystem.media.Shuffle();
        }

        public void Stop()
        {
            IntelliRoomSystem.media.Stop();
        }
    }
}
