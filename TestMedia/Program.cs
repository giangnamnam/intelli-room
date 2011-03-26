using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Media;

namespace TestMedia
{
    class Program
    {
        static void Main(string[] args)
        {
            IMediaPlayer media = new MediaPlayer();
            while(true)
            {
                string name = Console.ReadLine();

                media.LoadMediaArtist(name);
                media.Play();
                System.Threading.Thread.Sleep(100);
                Console.WriteLine(media.GetInfoPlayList());
                Console.WriteLine("-------");
            }
            
            
        }
    }
}
