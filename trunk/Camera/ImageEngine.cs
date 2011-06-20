using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;

namespace Camera
{
    public class ImageEngine : IImageEngine
    {
        public delegate void dlgImagen(List<System.Drawing.Rectangle> interestRegions);
 
        public event dlgImagen peopleDetected;
        public event dlgImagen dogsDetected;
        public event dlgImagen flowersDetected;

        public event Action<List<System.Drawing.Rectangle>> peopleDetected2;
        public event Func<int, int> a=;


        public event EventHandler<double> movementDetected;
        public event EventHandler<FaceResult> facesDetected;
        public event EventHandler<double> iluminanceEvent;
        public event EventHandler<LastResults> imageResult;

        public Image<Bgr, Byte> image;
        public Image<Bgr, Byte> lastImage;

        private Stopwatch time;
        private Thread thread;
        private LastResults lastResult;

        public ImageEngine() 
        {
            time = new Stopwatch();
            thread = new Thread(new ThreadStart(StartEngine));

            List<System.Drawing.Rectangle> people = null;
            //procesamiento que rellena people

            if (peopleDetected2 != null)
                peopleDetected2(people);
        }

        public LastResults LastResult
        {
            get { return lastResult; }
            set { lastResult = value; }
        }

        public void StartEngine()
        {
            if (!thread.IsAlive)
            {
                time.Start();
                thread.Start();
                while (thread.IsAlive)
                {
                    if (time.ElapsedMilliseconds >= Config.processMilliseconds)
                    {
                        time.Reset();
                        time.Start();
                        ProcessImage();
                    }
                    System.Threading.Thread.Sleep(20);
                }
            }
        }

        public void StopEngine()
        {
            if (thread.IsAlive)
            {
                thread.Abort();
            }
        }

        public static double GetIluminance()
        {
            Image<Bgr, Byte> image = Config.camera.GetImage();

            return ImageUtils.GetIluminance(image);
        }

        public static FaceResult FaceDetect()
        {
            Image<Bgr, Byte> image = Config.camera.GetImage();

            return ImageUtils.FaceDetect(image);
        }

        private void ProcessImage()
        {
            GetImage();

            LastResult = new LastResults(image.Copy());

            if (Config.calculeIluminance)
            {
                double iluminance = ImageUtils.GetIluminance(image);
                LastResult.iluminance = iluminance;
                if (iluminance >= Config.iluminanceEvent)
                {
                    //lanzar evento
                    iluminanceEvent.Invoke(null, iluminance);
                }
            }

            if (Config.calculeMovement)
            {
                double movement = ImageUtils.GetMovement(image, lastImage);
                LastResult.movement = movement;
                if (movement >= Config.isMovement)
                {
                    //lanzar evento (con algun argumento)
                    movementDetected.Invoke(null, movement);

                    if (Config.saveMovement)
                    {
                        ImageUtils.SavePicture(image);
                    }
                }
            }

            if (Config.calculeFace)
            {
                FaceResult faceResult = ImageUtils.FaceDetect(image);
                LastResult.faces = faceResult;
                LastResult.numberOfFaces = faceResult.GetNumberOfFaces();
                if (faceResult.FaceDetect())
                {
                    //lanzar evento
                    facesDetected.Invoke(null, faceResult);

                    if (Config.saveFaces)
                    {
                        faceResult.SaveAllFaces();
                    }
                }
            }

            imageResult.Invoke(null, lastResult);
        }

        public static void SetProcessMilliseconds(int millis)
        {
            Config.processMilliseconds = millis;
        }

        public static int GetProcessMilliseconds()
        {
            return Config.processMilliseconds;
        }

        public static void SetIsMovement(int movement)
        {
            Config.isMovement = movement;
        }

        public static int GetIsMovement()
        {
            return Config.isMovement;
        }

        public static void SetIluminanceEvent(int iluminance)
        {
            Config.iluminanceEvent = iluminance;
        }

        public static int GetIluminanceEvent()
        {
            return Config.iluminanceEvent;
        }

        public static void SetCalculeIluminance(bool calculeIluminance)
        {
            Config.calculeIluminance = calculeIluminance;
        }

        public static bool GetCalculeIluminance()
        {
            return Config.calculeIluminance;
        }

        public static void SetCalculeMovement(bool calculeMovement)
        {
            Config.calculeMovement = calculeMovement;
        }

        public static bool GetCalculeMovement()
        {
            return Config.calculeMovement;
        }

        public static void SetCalculeFace(bool calculeFaces)
        {
            Config.calculeFace = calculeFaces;
        }

        public static bool GetCalculeFace()
        {
            return Config.calculeFace;
        }

        public static void SetSaveMovement(bool saveMovement)
        {
            Config.saveMovement = saveMovement;
        }

        public static bool GetSaveMovement()
        {
            return Config.saveMovement;
        }

        public static void SetSaveFaces(bool saveFaces)
        {
            Config.saveFaces = saveFaces;
        }

        public static bool GetSaveFaces()
        {
            return Config.saveFaces;
        }

        private void GetImage()
        {
            if (lastImage == null)
            {
                lastImage = Config.camera.GetImage();
            }
            else
            {
                lastImage = image.Copy();
            }

            image = Config.camera.GetImage();
        }
    }
}
