using System;
using System.Collections.Generic;
using System.Drawing;
using System.Speech.Recognition;
using Data;
using Media;
using System.Threading;
using System.Threading.Tasks;

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
        public string Date()
        {
            return Utils.Time.GetStringDate();
        }

        public string Time()
        {
            return Utils.Time.GetStringTime();
        }

        public DateTime DateAndTime()
        {
            return Utils.Time.GetDate();
        }

        public int Temperature(string city)
        {
            return new Utils.Weather(city).TemperatureC;
        }

        public string Condition(string city)
        {
            return new Utils.Weather(city).Condition;
        }

        public int TemperatureFarenheit(string city)
        {
            return new Utils.Weather(city).TemperatureF;
        }

        public int Humidity(string city)
        {
            return new Utils.Weather(city).Humidity;
        }

        public string WindDirection(string city)
        {
            return new Utils.Weather(city).WindDirection;
        }

        public int WindSpeed(string city)
        {
            return new Utils.Weather(city).WindSpeed;
        }

        //LIGHTING

        public void DirectColor(String color)
        {
            IntelliRoomSystem.lighting.DirectColor(color);
        }

        public void DirectColor(byte r, byte g, byte b)
        {
            IntelliRoomSystem.lighting.DirectColor(r,g,b);
        }

        public void DirectColor(Color color)
        {
            IntelliRoomSystem.lighting.DirectColor(color);
        }

        public void GradientColor(byte r, byte g, byte b, int timeMillis)
        {
            IntelliRoomSystem.lighting.GradientColor(r, g, b, timeMillis);
        }

        public void GradientColor(string colorName, int timeMillis)
        {
            IntelliRoomSystem.lighting.GradientColor(colorName, timeMillis);
        }

        public void GradientColor(Color color, int timeMillis)
        {
            IntelliRoomSystem.lighting.GradientColor(color, timeMillis);
        }

        public void TurnOffLight()
        {
            IntelliRoomSystem.lighting.TurnOffLight();
        }

        public void TurnOnLight()
        {
            IntelliRoomSystem.lighting.TurnOnLight();
        }

        public void RandomColor(int timeMillis)
        {
            IntelliRoomSystem.lighting.RandomColor(timeMillis);
        }

        public void DesactiveRandomColor()
        {
            IntelliRoomSystem.lighting.DesactiveRandomColor();
        }

        public void RandomColor(bool active, int timeMillis)
        {
            IntelliRoomSystem.lighting.RandomColor(active, timeMillis);
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

        public void Rewind()
        {
            IntelliRoomSystem.media.Rewind();
        }

        public void Stop()
        {
            IntelliRoomSystem.media.Stop();
        }

        public string InfoAuthor()
        {
            return IntelliRoomSystem.media.GetInfoAuthor();
        }

        public string InfoAlbum()
        {
            return IntelliRoomSystem.media.GetInfoAlbum();
        }

        public string InfoDuration()
        {
            return IntelliRoomSystem.media.GetInfoDuration();
        }

        public MusicMedia InfoPlayList()
        {
            return IntelliRoomSystem.media.GetInfoPlayList();
        }

        public MusicMedia InfoMedia()
        {
            return IntelliRoomSystem.media.GetInfoMedia();
        }

        public string InfoTitle()
        {
            return IntelliRoomSystem.media.GetInfoTitle();
        }

        public void ChangeVolume(int volume)
        {
            IntelliRoomSystem.media.ChangeVolume(volume);
        }

        public void DecreaseVolume()
        {
            IntelliRoomSystem.media.DecreaseVolume();
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

        public void Mute()
        {
            IntelliRoomSystem.media.Mute();
        }

        public void LoadAllMedia()
        {
            IntelliRoomSystem.media.LoadAllMedia();
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

        public void Exit()
        {
            System.Environment.Exit(0);
        }

        //DATA
        public static List<string> GetMessages()
        {
            return Data.InfoMessages.GetAllTextMessages();
        }

    }

    public class TextCommand : Command
    {

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
    }

    public class SpeakCommand : Command
    {
        public bool esperaActiva;
        public string result;
        public string context;
        
        public void LoadAlbum()
        {
            IntelliRoomSystem.voiceEngine.LoadListGrammar(IntelliRoomSystem.media.GetAllAlbums(),"LoadAlbum");

            var cancelToken = new CancellationTokenSource();
            var task = Task.Factory.StartNew(() =>
            {
                IntelliRoomSystem.voiceEngine.speechRecognizer += new EventHandler<RecognitionEventArgs>(speechCommandRecognizer);

                esperaActiva = true;
                result = "";
                while (esperaActiva) ;
            }, cancelToken.Token).ContinueWith(x => {
                IntelliRoomSystem.voiceEngine.speechRecognizer -= new EventHandler<RecognitionEventArgs>(speechCommandRecognizer);
            }, TaskContinuationOptions.AttachedToParent);

            var a = task.Wait(8000);
            cancelToken.Cancel();

            if (task.IsCompleted && context == "LoadAlbum")
            {
                IntelliRoomSystem.media.LoadMediaAlbum(result);
            }

            IntelliRoomSystem.voiceEngine.LoadGrammar();
        }

        //public void LoadAuthor()
        //{
        //    IntelliRoomSystem.media.LoadMediaAuthor();
        //}

        //public void LoadGenre()
        //{
        //    IntelliRoomSystem.media.LoadMediaGenre();
        //}

        //public void LoadTitle()
        //{
        //    IntelliRoomSystem.media.LoadMediaTitle(nameSong);
        //}

        void speechCommandRecognizer(object sender, RecognitionEventArgs e)
        {
            result = e.Result.Text;
            context = e.Result.Grammar.Name;
            esperaActiva = false;
        }
    }
}
