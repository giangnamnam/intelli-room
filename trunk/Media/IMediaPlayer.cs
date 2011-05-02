using System;
namespace Media
{
    public interface IMediaPlayer
    {
        void ChangeVolume(int newVolume);
        int DecreaseVolume();
        void Forward();
        System.Collections.Generic.List<string> GetAllAlbums();
        System.Collections.Generic.List<string> GetAllAuthors();
        System.Collections.Generic.List<string> GetAllGenres();
        System.Collections.Generic.List<string> GetAllTitles();
        string GetInfoAlbum();
        string GetInfoAuthor();
        string GetInfoDuration();
        string GetInfoGenre();
        MusicMedia GetInfoPlayList();
        string GetInfoTitle();
        int GetVolume();
        int IncreaseVolume();
        void LoadAllMedia();
        void LoadMediaAlbum(string nameAlbum);
        void LoadMediaAuthor(string nameAuthor);
        void LoadMediaGenre(string nameGenre);
        void LoadMediaTitle(string nameTitle);
        void LoadUrl(string url);
        void Mute();
        void Pause();
        void Play();
        void Rewind();
        void Stop();
    }
}
