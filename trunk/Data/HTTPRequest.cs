using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;

namespace Data
{
    public class HTTPRequest
    {

        public static XmlDocument GetXML (string url)
        {
            try
            {
                HttpWebRequest request = ((HttpWebRequest)WebRequest.Create(url));

                request.UserAgent = "Mozilla/5.0 (X11; U; Linux i686; es-ES; rv:1.9.0.2) Gecko/2008092313 Ubuntu/9.25 (jaunty) Firefox/3.8";
                request.Accept = "es-es,es;q=0.8,en-us;q=0.5,en;q=0.3";

                WebResponse response = request.GetResponse();

                StreamReader stream = new StreamReader(response.GetResponseStream());

                XmlDocument xml = new XmlDocument();
                xml.Load(stream);
                return xml;
            }

            catch (Exception)
            {
                return null;
            }
        }
        

    }
}
