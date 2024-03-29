﻿using System;
namespace IntelliRoom
{
    public interface IIntelliRoom
    {
        void AddAction(string nameEvent, string command);
        void AddGrammar(System.Speech.Recognition.Grammar grammar);
        void AddGrammarList(System.Collections.Generic.List<string> list, string context);
        void AddTask(string command, DateTime date);
        void AddTask(string command, int day, int month, int year, int hour, int minute);
        void AddTask(string command, int hour, int minute);
        void ChangePrecisionRecognizer(int precision);
        void ChangeRangeInTemperatureEvent(int min, int max);
        void ChangeVolume(int volume);
        string Condition();
        bool ConfigCameraGetCalculeFace();
        bool ConfigCameraGetCalculeIluminance();
        bool ConfigCameraGetCalculeMovement();
        int ConfigCameraGetHighIluminanceEvent();
        int ConfigCameraGetIsMovement();
        int ConfigCameraGetLowIluminanceEvent();
        int ConfigCameraGetProcessMilliseconds();
        bool ConfigCameraGetSaveFaces();
        bool ConfigCameraGetSaveMovement();
        void ConfigCameraSetCalculeFace(bool calculeFaces);
        void ConfigCameraSetCalculeIluminance(bool calculeIluminance);
        void ConfigCameraSetCalculeMovement(bool calculeMovement);
        void ConfigCameraSetHighIluminanceEvent(int iluminance);
        void ConfigCameraSetIsMovement(int movement);
        void ConfigCameraSetLowIluminanceEvent(int iluminance);
        void ConfigCameraSetProcessMilliseconds(int millis);
        void ConfigCameraSetSaveFaces(bool saveFaces);
        void ConfigCameraSetSaveMovement(bool saveMovement);
        string Date();
        DateTime DateAndTime();
        void DecreaseVolume();
        void DeleteGrammar();
        void DesactiveRandomColor();
        void DictationMode();
        void DirectColor(byte red, byte green, byte blue);
        void DirectColor(System.Drawing.Color color);
        void DirectColor(string color);
        void Exit();
        Camera.FaceResult FaceDetect();
        void Forward();
        System.Collections.Generic.List<string> GetAllAlbums();
        System.Collections.Generic.List<string> GetAllAuthors();
        System.Collections.Generic.List<string> GetAllGenres();
        System.Collections.Generic.List<string> GetAllSongs();
        string GetConfigCamera();
        Camera.LastResults GetLastResults();
        double GetMovement();
        double GetRoomIluminance();
        int GetVolume();
        void GradientColor(byte red, byte green, byte blue, int timeMillis);
        void GradientColor(System.Drawing.Color color, int timeMillis);
        void GradientColor(string colorName, int timeMillis);
        int Humidity();
        void IncreaseVolume();
        string InfoAlbum();
        string InfoAuthor();
        string InfoDuration();
        global::Media.MusicMedia InfoMedia();
        global::Media.MusicMedia InfoPlayList();
        string InfoTitle();
        void LoadAlbum(string nameAlbum);
        void LoadAllMedia();
        void LoadAuthor(string nameAuthor);
        void LoadGenre(string nameGenre);
        void LoadGrammar();
        void LoadGrammarList(System.Collections.Generic.List<string> list, string context);
        void LoadMediaUrl(string url);
        void LoadTitle(string nameSong);
        void MaximumVolume();
        void MinimumVolume();
        void Mute();
        int NumberFacesDetect();
        void Pause();
        void Play();
        void RandomColor(bool active, int timeMillis);
        void RandomColor(int timeMillis);
        void ReloadGrammar();
        void Rewind();
        void Sleep(int millis);
        void Speak(string speakText);
        void StartProcessImage();
        void Stop();
        void StopProcessImage();
        void SwitchOffAllDevices();
        void SwitchOffDevice(int device);
        void SwitchOnAllDevices();
        void SwitchOnDevice(int device);
        int Temperature();
        int TemperatureFahrenheit();
        string Time();
        void TurnOffLight();
        void TurnOnLight();
        void WeatherCity(string city);
        string WindDirection();
        int WindSpeed();
    }
}
