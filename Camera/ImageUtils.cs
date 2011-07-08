using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using Data;


namespace Camera
{
    class ImageUtils
    {
        public static double GetIluminance(Image<Bgr, Byte> image)
        {
            //transformamos la imagen de RGB to HSV
            Image<Hsv, Byte> imageHSV = image.Convert<Hsv, Byte>();
            
            //calculo la media del componente "V"
            double value = imageHSV.GetAverage().Value;

            //para darlo en funcion de porcentaje (0 a 100)
            return value/255*100;
        }

        public static double GetMovement(Image<Bgr, Byte> image, Image<Bgr, Byte> lastImage)
        {
            //aplicamos una media para "solucionar" posibles errores que puedan ser cometidos por la camara
            Image<Bgr, byte> imageA = image.SmoothMedian(9);
            Image<Bgr, byte> imageL = lastImage.SmoothMedian(9);

            //hacemos sus diferencias
            Image<Bgr, byte> imageSub1 = imageA.Sub(imageL);
            Image<Bgr, byte> imageSub2 = imageL.Sub(imageA);
            
            //sumamos sus diferencias
            Image<Bgr, byte> imageOr = imageSub1.Or(imageSub2);

            //vemos cuanto valor tiene esa imagen reutilizando la funcion GetIluminance
            return GetIluminance(imageOr);
        }

        public static FaceResult FaceDetect(Image<Bgr, Byte> image)
        {
            FaceResult result = new FaceResult(image.Copy());

            //convierto a escala de grises
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>();

            //normalizamos el brillo y mejoramos el contraste
            gray._EqualizeHist();

            //leemos el XML con el entrenamiento (en nuestros caso usamos uno de caras frontales)
            HaarCascade face = new HaarCascade(Directories.GetHaarCascade());

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

        public static void SavePicture(Image<Bgr, Byte> image)
        {
            int picName = Directories.GetNextNamePicture();
            image.Save(Directories.GetPicturesDirectory() + picName.ToString()+".jpg");
        }

        public static void SaveFace(Image<Bgr, Byte> image, Rectangle rectangle)
        {
            Image<Bgr, Byte> imageCopy = image.Copy(rectangle);
            int picFace = Directories.GetNextNameFace();
            imageCopy.Save(Directories.GetFacesDirectory() + picFace.ToString() + ".jpg");
        }
    }

    public class FaceResult
    {
        Image<Bgr, Byte> image;
        List<Rectangle> faces;

        public FaceResult(Image<Bgr, Byte> image)
        {
            faces = new List<Rectangle>();
            image.ROI = Rectangle.Empty;
            this.image = image.Copy();
        }

        public Image<Bgr, Byte> Image
        {
            get { return image; }
            set { image = value; }
        }

        public List<Rectangle> Faces
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

        public void SaveAllFaces()
        {
            foreach(Rectangle rect in faces)
            {
                ImageUtils.SaveFace(image, rect);
            }
        }

        public override string ToString()
        {
            return "En la captura encontramos " + GetNumberOfFaces() + " caras";
        }
    }
}
