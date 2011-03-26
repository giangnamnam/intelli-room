using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Image
{
    static class Config
    {
        public static Camera camera = new Camera();
        public static int captureFPS = 4;
        public static int isMovement = 10;
        public static bool calculeIluminance = true;
        public static bool calculeMovement = true;
        public static bool calculeFace = true;
        public static bool saveMovement = true;
        public static bool saveFaces = true;

    }
}
