﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using Data;

namespace Camera
{
    public class ImageEngine : IImageEngine
    {
        public event Action<FaceResult> peopleDetected;
        public event Action<double> movementDetected;
        public event Action<double> lowIluminanceEvent;
        public event Action<double> highIluminanceEvent;
        public event Action<LastResults> finishImageProcess;

        public Image<Bgr, Byte> image;
        public Image<Bgr, Byte> lastImage;

        private Stopwatch time;
        private Thread thread;
        private LastResults lastResult;

        public ImageEngine() 
        {
            time = new Stopwatch();
            thread = new Thread(new ThreadStart(Engine));
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
            }
        }

        public void StopEngine()
        {
            if (thread.IsAlive)
            {
                thread.Abort();
            }
        }

        private void Engine()
        {
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

        public static double GetIluminance()
        {
            Image<Bgr, Byte> image = Config.camera.GetImage();

            return ImageUtils.GetIluminance(image);
        }

        public static double GetMovement()
        {
            Image<Bgr, Byte> lastimage = Config.camera.GetImage();
            Thread.Sleep(Config.processMilliseconds);
            Image<Bgr, Byte> image = Config.camera.GetImage();
            return ImageUtils.GetMovement(image, lastimage);
        }

        public static FaceResult FaceDetect()
        {
            Image<Bgr, Byte> image = Config.camera.GetImage();

            return ImageUtils.FaceDetect(image);
        }

        public static void SaveImage()
        {
            int picName = Directories.GetNextNamePicture();
            Config.camera.GetImage().Save(Directories.GetPicturesDirectory() + picName.ToString()+".jpg");
        }

        private void ProcessImage()
        {
            GetImage();

            LastResult = new LastResults(image);

            if (Config.calculeIluminance)
            {
                double iluminance = ImageUtils.GetIluminance(image);
                LastResult.iluminance = iluminance;
                if (iluminance <= Config.lowIluminanceEvent && lowIluminanceEvent != null)
                {
                    //Data.InfoMessages.InformationMessage("La iluminacion es " + iluminance);
                    lowIluminanceEvent(iluminance);
                }

                if (iluminance >= Config.highIluminanceEvent && highIluminanceEvent != null)
                {
                    //Data.InfoMessages.InformationMessage("La iluminacion es " + iluminance);
                    highIluminanceEvent(iluminance);
                }
            }

            if (Config.calculeMovement)
            {
                double movement = ImageUtils.GetMovement(image, lastImage);
                LastResult.movement = movement;
                if (movement >= Config.isMovement)
                {
                    //lanzar evento
                    if (movementDetected != null)
                    {
                        //Data.InfoMessages.InformationMessage("Movimiento " + movement);
                        movementDetected(movement);
                    }

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
                    if(peopleDetected != null)
                    {
                        peopleDetected(faceResult);
                    }

                    if (Config.saveFaces)
                    {
                        faceResult.SaveAllFaces();
                    }
                }
            }
            if (finishImageProcess != null)
            {
                finishImageProcess(LastResult);
                //Data.InfoMessages.InformationMessage(LastResult.ToString());
            }
           
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

        public static void SetLowIluminanceEvent(int iluminance)
        {
            Config.lowIluminanceEvent = iluminance;
        }

        public static int GetLowIluminanceEvent()
        {
            return Config.lowIluminanceEvent;
        }

        public static void SetHighIluminanceEvent(int iluminance)
        {
            Config.highIluminanceEvent = iluminance;
        }

        public static int GetHighIluminanceEvent()
        {
            return Config.highIluminanceEvent;
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
