using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Image
{
    public class Camera
    {
        private Capture capture;

        public Camera()
        {
            capture = new Capture(0);
            
        }

        public Image<Bgr, byte> GetImage()
        {
            return capture.QueryFrame();
        }

        public List<string> GetDispositives()
        {
            List<string> dispositives = new List<string>();

            //BUSCAR NOMBRES CAMARAS

            return dispositives;
        }

    }
}
