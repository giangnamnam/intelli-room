using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;


namespace Image
{
    class ImageUtils
    {
        public static double GetIluminance(Image<Bgr, Byte> image)
        {
            Image<Hsv, Byte> imageHSV = image.Convert<Hsv, Byte>();
            //calculo la media del componente "V"


            return 0;
        }

        public static double GetMovement(Image<Bgr, Byte> image, Image<Bgr, Byte> lastImage)
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

        public static FaceResult FaceDetect(Image<Bgr, Byte> image)
        {
            FaceResult result = new FaceResult();
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>(); //convierto a escala de grises

            //normalizamos el brillo y mejoramos el contraste
            gray._EqualizeHist();

            //leemos el XML de entrenamiento de caras (en nuestros caso usamos uno de caras frontales)
            HaarCascade face = new HaarCascade("HaarCascade\\haarcascade_frontalface_alt_tree.xml");

            //Detectamos las caras de la imagen en blanco y negro
            //El primer dimensional contiene el canal (solo nos centraremos en el canal 0, porque estamos trabajando en blanco y negro)
            //El segundo dimensional es el indice del rectangulo
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.1, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            //Por cada rectangulo detectado, lo incluimos en el resultado
            foreach (MCvAvgComp f in facesDetected[0])
            {
                result.AddFace(f.rect);
            }

            return result;
        }

        public static void SaveImage(Image<Bgr, Byte> image)
        {
            Random r = new Random();
            image.Save("PictureSave\\" + r.Next().ToString());
        }

        public static void SaveImage(Image<Bgr, Byte> image, Rectangle rectangle)
        {
            image.GrabCut(rectangle, 1);
            Random r = new Random();
            image.Save("PictureSave\\" + r.Next().ToString());
        }
    }

    class FaceResult
    {
        List<Rectangle> faces;

        public FaceResult()
        {
            faces = new List<Rectangle>();
        }

        private List<Rectangle> Faces
        {
            get { return faces; }
        }

        public void AddFace(Rectangle rect)
        {
            faces.Add(rect);
        }

        public int GetNumberOfFaces()
        {
            return faces.Count;
        }

        public bool FaceDetect()
        {
            return GetNumberOfFaces() != 0;
        }
    }
}
