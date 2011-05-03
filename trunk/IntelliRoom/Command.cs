using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Media;
using System.Drawing;
using Data;

namespace IntelliRoom
{
    public class Command
    {
        //INIT
        public static void Init()
        {
            IntelliRoomSystem.InitSystem();
        }

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

        public void DeleteGrammar()
        {
            IntelliRoomSystem.voiceEngine.DeleteAllGrammars();
        }

        public void LoadGrammar()
        {
            IntelliRoomSystem.voiceEngine.LoadGrammar();
        }

        //UTILS
        public string GetDate()
        {
            return Time.GetDate();
        }

        public string GetTime()
        {
            return Time.GetTime();
        }

        public int GetTemperature(string city)
        {
            return new Weather(city).TemperatureC;
        }

        public string GetCondition(string city)
        {
            return new Weather(city).Condition;
        }

        public int GetTemperatureFarenheit(string city)
        {
            return new Weather(city).TemperatureF;
        }

        public int GetHumidity(string city)
        {
            return new Weather(city).Humidity;
        }

        public string GetWindDirection(string city)
        {
            return new Weather(city).WindDirection;
        }

        public int GetWindSpeed(string city)
        {
            return new Weather(city).WindSpeed;
        }

        //LIGHTING

        public void SetDirectColor(String color)
        {
            IntelliRoomSystem.lighting.SetDirectColor(color);
        }

        public void SetDirectColor(byte r, byte g, byte b)
        {
            IntelliRoomSystem.lighting.SetDirectColor(r,g,b);
        }

        //public void SetDirectColor(Color color)
        //{
        //    IntelliRoomSystem.lighting.SetDirectColor(color);
        //}

        public void SetDegradedColor(byte r, byte g, byte b, int timeMillis)
        {
            IntelliRoomSystem.lighting.SetDegradedColor(r, g, b, timeMillis);
        }

        //public void SetDegradedColor(Color color, int timeMillis)
        //{
        //    IntelliRoomSystem.lighting.SetDegradedColor(color, timeMillis);
        //}

        public void SetDegradedColor(string colorName, int timeMillis)
        {
            IntelliRoomSystem.lighting.SetDegradedColor(colorName,timeMillis);
        }

        public void TurnOffLight()
        {
            IntelliRoomSystem.lighting.TurnOffLight();
        }

        public void TurnOnLight()
        {
            IntelliRoomSystem.lighting.TurnOnLight();
        }

        //DEVICE

        public void SwitchOnDevice(int device)
        {
            IntelliRoomSystem.device.SwitchOn(device);
        }

        public void SwitchOffDevice(int device)
        {
            IntelliRoomSystem.device.SwitchOff(device);
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

        public void DecreaseVolume()
        {
            IntelliRoomSystem.media.DecreaseVolume();
        }

        public string GetInfoArtist()
        {
            return IntelliRoomSystem.media.GetInfoAuthor();
        }

        public string GetInfoDisc()
        {
            return IntelliRoomSystem.media.GetInfoAlbum();
        }

        public string GetInfoDuration()
        {
            return IntelliRoomSystem.media.GetInfoDuration();
        }

        public MusicMedia GetInfoPlayList()
        {
            return IntelliRoomSystem.media.GetInfoPlayList();
        }

        public MusicMedia GetInfoMedia()
        {
            return IntelliRoomSystem.media.GetInfoMedia();
        }

        public string GetInfoTitle()
        {
            return IntelliRoomSystem.media.GetInfoTitle();
        }

        public void IncreaseVolume()
        {
            IntelliRoomSystem.media.IncreaseVolume();
        }

        public int GetVolume()
        {
            return IntelliRoomSystem.media.GetVolume();
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

        public void LoadAuthor(string nameArtist)
        {
            IntelliRoomSystem.media.LoadMediaAuthor(nameArtist);
        }

        public void LoadGenre(string nameGenre)
        {
            IntelliRoomSystem.media.LoadMediaGenre(nameGenre);
        }

        public void LoadTitle(string nameSong)
        {
            IntelliRoomSystem.media.LoadMediaTitle(nameSong);
        }

        public void LoadUrl(string url)
        {
            IntelliRoomSystem.media.LoadUrl(url);
        }
        public void Mute()
        {
            IntelliRoomSystem.media.Mute();
        }

        public void Rewind()
        {
            IntelliRoomSystem.media.Rewind();
        }

        public void Stop()
        {
            IntelliRoomSystem.media.Stop();
        }

        public List<string> GetAllAuthors()
        {
            return IntelliRoomSystem.media.GetAllAuthors();
        }

        public List<string> GetAllSongs()
        {
            return IntelliRoomSystem.media.GetAllTitles();
        }

        public List<string> GetAllGenres()
        {
            return IntelliRoomSystem.media.GetAllGenres();
        }

        public List<string> GetAllAlbums()
        {
            return IntelliRoomSystem.media.GetAllAlbums();
        }

        //CAMERA

        public void StartProcessImage()
        {
            IntelliRoomSystem.camera.StartEngine();
        }

        public void StopProcessImage()
        {
            IntelliRoomSystem.camera.StopEngine();
        }

        //FUNCTIONS
        public void ChangeLanguaje()
        {
        }

        public List<string> GetLanguajeList()
        {
            return Languages.GetLanguages();
        }

        public void Shutdown()
        {
        }

        public void CloseAplication()
        {
        }

        public void Suspend()
        {
        }

        public void Sleep()
        {
        }

        public void WakeUp()
        {
        }

        public void Hibernate()
        {
        }

        public void ActiveRecognizer()
        {
        }

        public void DesactiveRecognizer()
        {
        }

        public void ActiveSynthesizer()
        {
        }

        public void DesactiveSynthesizer()
        {
        }
    }
}
