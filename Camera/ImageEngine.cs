using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Image
{
    class ImageEngine
    {
        public event EventHandler movementDetected;
        public event EventHandler facesDetected;
        public event EventHandler iluminanceEvent;

        public Image<Bgr, Byte> image;
        public Image<Bgr, Byte> lastImage;

        public ImageEngine() { }

        public void ProcessImage()
        {
            GetImage();

            if (Config.calculeIluminance)
            {
                double iluminance = ImageUtils.GetIluminance(image);
                LastResults.iluminance = iluminance;
                if (iluminance >= Config.iluminanceEvent)
                {
                    
                }
            }

            if (Config.calculeMovement)
            {
                double movement = ImageUtils.GetMovement(image, lastImage);
                LastResults.movement = movement;
                if (movement >= Config.isMovement)
                {

                }
            }

            if (Config.calculeFace)
            {
                FaceResult faceResult = ImageUtils.FaceDetect(image);
                LastResults.faces = faceResult;
                LastResults.numberOfFaces = faceResult.GetNumberOfFaces();
                if (faceResult.FaceDetect())
                {

                }
            }

        }
        private void GetImage()
        {
            if (lastImage == null)
            {
                lastImage = Config.camera.GetImage();
            }

            image = Config.camera.GetImage();
        }
    }
}
