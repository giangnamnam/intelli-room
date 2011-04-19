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
        string GetInfoArtist();
        string GetInfoDisc();
        string GetInfoDuration();
        string GetInfoPlayList();
        string GetInfoTitle();
        int IncreaseVolume();
        void LoadAllMedia();
        void LoadMediaAlbum(string nameAlbum);
        void LoadMediaArtist(string nameArtist);
        void LoadMediaGenre(string nameGenre);
        void LoadMediaSong(string nameSong);
        void LoadUrl(string url);
        void Mute();
        void NotRepeat();
        void Pause();
        void Play();
        void RepeatAll();
        void RepeatOne();
        void Rewind();
        void Shuffle();
        void Stop();
    }
}
