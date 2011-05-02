using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Camera
{
    public class ImageEngine : IImageEngine
    {
        public event EventHandler movementDetected;
        public event EventHandler facesDetected;
        public event EventHandler iluminanceEvent;

        public Image<Bgr, Byte> image;
        public Image<Bgr, Byte> lastImage;

        private Stopwatch time;
        private Thread thread;

        public ImageEngine() 
        {
            time = new Stopwatch();
            thread = new Thread(new ThreadStart(StartEngine));
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

        private void ProcessImage()
        {
            GetImage();

            LastResults.image = image.Copy();

            if (Config.calculeIluminance)
            {
                double iluminance = ImageUtils.GetIluminance(image);
                LastResults.iluminance = iluminance;
                if (iluminance >= Config.iluminanceEvent)
                {
                    //lanzar evento
                }
            }

            if (Config.calculeMovement)
            {
                double movement = ImageUtils.GetMovement(image, lastImage);
                LastResults.movement = movement;
                if (movement >= Config.isMovement)
                {
                    //lanzar evento

                    if (Config.saveMovement)
                    {
                        ImageUtils.SavePicture(image);
                    }
                }
            }

            if (Config.calculeFace)
            {
                FaceResult faceResult = ImageUtils.FaceDetect(image);
                LastResults.faces = faceResult;
                LastResults.numberOfFaces = faceResult.GetNumberOfFaces();
                if (faceResult.FaceDetect())
                {
                    //lanzar evento
                    if (Config.saveFaces)
                    {
                        foreach (Rectangle rect in faceResult.Faces)
                        {
                            ImageUtils.SaveFace(image,rect);
                        }
                    }
                }
            }
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
