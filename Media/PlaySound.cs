using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;

namespace Media
{
    class PlaySounds
    {
        private WindowsMediaPlayer sound;
        public event EventHandler finishSound;

        public PlaySounds(string URLsound)
        {
            sound = new WindowsMediaPlayer();
            sound.URL = URLsound;
            sound.StatusChange += new _WMPOCXEvents_StatusChangeEventHandler(sound_StatusChange);
        }

        void sound_StatusChange()
        {
            finishSound.Invoke(null, null);
        }
    }
}
