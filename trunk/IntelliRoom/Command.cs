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
        //evento de informacion
        public event EventHandler<EventArgs> informationEvent;

        public void sendInformationEvent(object sender, EventArgs e)
        {
            this.informationEvent(sender, e);
        }
        //fin evento informacion

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
            return Utils.GetDate();
        }

        public string GetTime()
        {
            return Utils.GetTime();
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

        public void SetDirectColor(Color color)
        {
            IntelliRoomSystem.lighting.SetDirectColor(color);
        }

        public void SetDegradedColor(byte r, byte g, byte b, int timeMillis)
        {
            IntelliRoomSystem.lighting.SetDegradedColor(r, g, b, timeMillis);
        }

        public void SetDegradedColor(Color color, int timeMillis)
        {
            IntelliRoomSystem.lighting.SetDegradedColor(color, timeMillis);
        }

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
