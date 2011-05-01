using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Camera
{
    static class Config
    {
        public static Camera camera = new Camera();

        public static int processMilliseconds = 1000; //numero de milisegundos que deben de pasar para procesar una nueva imagen

        public static int isMovement = 10;
        public static int iluminanceEvent = 10;

        public static bool calculeIluminance = true;
        public static bool calculeMovement = true;
        public static bool calculeFace = true;

        public static bool saveMovement = true;
        public static bool saveFaces = true;
    }

    class LastResults
    {
        public static double movement;
        public static FaceResult faces;
        public static double iluminance;
        public static int numberOfFaces;
        public static Image<Bgr, Byte> image;
    }
}
