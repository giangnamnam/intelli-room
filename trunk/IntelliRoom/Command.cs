using System;
using System.Collections.Generic;
using System.Drawing;
using System.Speech.Recognition;
using Data;
using Media;
using System.Threading;
using System.Threading.Tasks;
using Camera;

namespace IntelliRoom
{
    public class Command : IIntelliRoom
    {
        //INIT
        public static void Init()
        {
            IntelliRoomSystem.InitSystem();
        }

        //VOICE
        public void Speak(string speakText)
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

        public void LoadGrammarList(List<string> list, string context)
        {
            IntelliRoomSystem.voiceEngine.LoadListGrammar(list, context);
        }

        public void AddGrammarList(List<string> list, string context)
        {
            IntelliRoomSystem.voiceEngine.AddListGrammar(list, context);
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
            return IntelliRoomSystem.weather.TemperatureC;
        }

        public string Condition(string city)
        {
            return IntelliRoomSystem.weather.Condition;
        }

        public int TemperatureFahrenheit(string city)
        {
            return IntelliRoomSystem.weather.TemperatureF;
        }

        public int Humidity(string city)
        {
            return IntelliRoomSystem.weather.Humidity;
        }

        public string WindDirection(string city)
        {
            return IntelliRoomSystem.weather.WindDirection;
        }
        
        public int WindSpeed(string city)
        {
            return IntelliRoomSystem.weather.WindSpeed;
        }

        public void ChangeRangeInTemperatureEvent(int min, int max)
        {
            IntelliRoomSystem.weather.MaxTemperatureEvent = max;
            IntelliRoomSystem.weather.MinTemperatureEvent = min;
        }


        //LIGHTING

        public void DirectColor(String color)
        {
            IntelliRoomSystem.lighting.DirectColor(color);
        }

        public void DirectColor(byte red, byte green, byte blue)
        {
            IntelliRoomSystem.lighting.DirectColor(red,green,blue);
        }

        public void DirectColor(Color color)
        {
            IntelliRoomSystem.lighting.DirectColor(color);
        }

        public void GradientColor(byte red, byte green, byte blue, int timeMillis)
        {
            IntelliRoomSystem.lighting.GradientColor(red, green, blue, timeMillis);
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

        public void LoadAlbum(string nameAlbum)
        {
            IntelliRoomSystem.media.LoadMediaAlbum(nameAlbum);
        }

        public void LoadAuthor(string nameAuthor)
        {
            IntelliRoomSystem.media.LoadMediaAuthor(nameAuthor);
        }

        public void LoadGenre(string nameGenre)
        {
            IntelliRoomSystem.media.LoadMediaGenre(nameGenre);
        }

        public void LoadTitle(string nameSong)
        {
            IntelliRoomSystem.media.LoadMediaTitle(nameSong);
        }

        public void LoadMediaUrl(string url)
        {
            IntelliRoomSystem.media.LoadUrl(url);
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

        public double GetRoomIluminance()
        {
            return Camera.ImageEngine.GetIluminance();
        }

        public FaceResult FaceDetect()
        {
            return Camera.ImageEngine.FaceDetect();
        }

        public int NumberFacesDetect()
        {
            return Camera.ImageEngine.FaceDetect().Faces.Count;
        }

        public LastResults GetLastResults()
        {
            return IntelliRoomSystem.camera.LastResult;
        }

        public void ConfigCameraSetProcessMilliseconds(int millis)
        {
            Camera.ImageEngine.SetProcessMilliseconds(millis);
        }

        public int ConfigCameraGetProcessMilliseconds()
        {
            return Camera.ImageEngine.GetProcessMilliseconds();
        }

        public void ConfigCameraSetIsMovement(int movement)
        {
            Camera.ImageEngine.SetIsMovement(movement);
        }

        public int ConfigCameraGetIsMovement()
        {
            return Camera.ImageEngine.GetIsMovement();
        }

        public void ConfigCameraSetIluminanceEvent(int iluminance)
        {
            Camera.ImageEngine.SetIluminanceEvent(iluminance);
        }

        public int ConfigCameraGetIluminanceEvent()
        {
            return Camera.ImageEngine.GetIluminanceEvent();
        }

        public void ConfigCameraSetCalculeIluminance(bool calculeIluminance)
        {
            Camera.ImageEngine.SetCalculeIluminance(calculeIluminance);
        }

        public bool ConfigCameraGetCalculeIluminance()
        {
            return Camera.ImageEngine.GetCalculeIluminance();
        }

        public void ConfigCameraSetCalculeMovement(bool calculeMovement)
        {
            Camera.ImageEngine.SetCalculeMovement(calculeMovement);
        }

        public bool ConfigCameraGetCalculeMovement()
        {
            return Camera.ImageEngine.GetCalculeMovement();
        }

        public void ConfigCameraSetCalculeFace(bool calculeFaces)
        {
            Camera.ImageEngine.SetCalculeFace(calculeFaces);
        }

        public bool ConfigCameraGetCalculeFace()
        {
            return Camera.ImageEngine.GetCalculeFace();
        }

        public void ConfigCameraSetSaveMovement(bool saveMovement)
        {
            Camera.ImageEngine.SetSaveMovement(saveMovement);
        }

        public bool ConfigCameraGetSaveMovement()
        {
            return Camera.ImageEngine.GetSaveFaces();
        }

        public void ConfigCameraSetSaveFaces(bool saveFaces)
        {
            Camera.ImageEngine.SetSaveFaces(saveFaces);
        }

        public bool ConfigCameraGetSaveFaces()
        {
            return Camera.ImageEngine.GetSaveFaces();
        }

        public string GetConfigCamera()
        {
            string result = "";
            result +="ProcessMilliseconds = "+Camera.ImageEngine.GetProcessMilliseconds()+"\n";
            result += "IsMovement = " + Camera.ImageEngine.GetIsMovement() + "\n";
            result += "IluminanceEvent = " + Camera.ImageEngine.GetIluminanceEvent() + "\n";
            result += "CalculeIluminance? = " + Camera.ImageEngine.GetCalculeIluminance() + "\n";
            result += "CalculeMovement? = " + Camera.ImageEngine.GetCalculeMovement() + "\n";
            result += "CalculeFace? = " + Camera.ImageEngine.GetCalculeFace() + "\n";
            result += "SaveMovement? = " + Camera.ImageEngine.GetSaveMovement() + "\n";
            result += "SaveFaces? = " + Camera.ImageEngine.GetSaveFaces() + "\n";
            return result;
        }

        //PROGRAMMER
        public void AddTask(string command, DateTime date)
        {
            IntelliRoomSystem.progammer.AddTask(command, date);
        }

        public void AddTask(string command, int day, int month, int year, int hour, int minute)
        {
            IntelliRoomSystem.progammer.AddTask(command, day, month, year, hour, minute);
        }

        public void AddTask(string command, int hour, int minute)
        {
            IntelliRoomSystem.progammer.AddTask(command, hour, minute);
        }

        //EVENTS
        public void AddAction(string nameEvent, string command)
        {
            IntelliRoomSystem.events.AddAction(nameEvent, command);
        }

        //FUNCTIONS
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
