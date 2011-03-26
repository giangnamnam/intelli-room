using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;


namespace Image
{
    class Image
    {
        public event EventHandler movementDetected;
        public event EventHandler facesDetected;
        public Image<Bgr, Byte> image;
        public Image<Bgr, Byte> lastImage;

        public Image() { }

        public ImageResult ProcessImage()
        {
            ImageResult ir = new ImageResult();
            GetImage();

            if (Config.calculeIluminance)
            {
                ir.iluminance = GetIluminance(image);
            }

            if (Config.calculeMovement)
            {
                ir.movement = GetMovement(image,lastImage);
            }

            if (Config.calculeFace)
            {
                ir.faceNumber = FaceDetect(image);
            }

            return ir;
        }

        public Image<Bgr, Byte> GetImage()
        {
            if (image != null)
            {
                lastImage = image;
            }
            this.image = Config.camera.GetImage();
            return image;
        }

        public double GetIluminance(Image<Bgr, Byte> image)
        {
            return 0;
        }

        public double GetMovement(Image<Bgr, Byte> image, Image<Bgr, Byte> lastImage)
        {
            Image<Bgr, byte> imageA = image.SmoothMedian(9);
            Image<Bgr, byte> imageL = lastImage.SmoothMedian(9);

            Image<Bgr, byte> imageSub1 = imageA.Sub(imageL);
            Image<Bgr, byte> imageSub2 = imageL.Sub(imageA);
            
            //LOQUITO ME TIENE ESTO
            IImage imageOr = imageSub1.Or(imageSub2);

            DenseHistogram histogram = new DenseHistogram(256, new RangeF());

            //histogram.Calculate(imageOr[], false, null);


            return 0;

        }

        public int FaceDetect(Image<Bgr, Byte> image)
        {
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>(); //convierto a escala de grises

            //normalizes brightness and increases contrast of the image
            gray._EqualizeHist();

            //Read the HaarCascade objects
            HaarCascade face = new HaarCascade("HaarCascade\\haarcascade_frontalface_alt_tree.xml");

            //Detect the faces  from the gray scale image and store the locations as rectangle
            //The first dimensional is the channel
            //The second dimension is the index of the rectangle in the specific channel
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.1, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            foreach (MCvAvgComp f in facesDetected[0])
            {
                //draw the face detected in the 0th (gray) channel with blue color
                image.Draw(f.rect, new Bgr(Color.Blue), 2);

                //Set the region of interest on the faces
                gray.ROI = f.rect;

            }
            return 0;
        }

        public void SaveImage(Image<Bgr, Byte> image)
        {
            Random r = new Random();
            image.Save("PictureSave\\" + r.Next().ToString());
        }
        public void SaveImage(Image<Bgr, Byte> image, Rectangle rectangle)
        {
            image.GrabCut(rectangle, 1);
            Random r = new Random();
            image.Save("PictureSave\\" + r.Next().ToString());
        }
    }

    class ImageResult
    {
        public double iluminance;
        public double movement;
        public int faceNumber;
    }
}
