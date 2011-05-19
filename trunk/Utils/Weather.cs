using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using Data;

namespace Utils
{
    public class Weather
    {
        private String condition;
        private int temperatureF;
        private int temperatureC;
        private int humidity;
        private String windDirection;
        private int windSpeed;

        public String Condition
        {
            get { return condition; }
            private set { condition = value; }
        }
        public int TemperatureF
        {
            get { return temperatureF; }
            private set { temperatureF = value; }
        }
        public int TemperatureC
        {
            get { return temperatureC; }
            private set { temperatureC = value; }
        }
        public int Humidity
        {
            get { return humidity; }
            private set { humidity = value; }
        }
        public String WindDirection
        {
            get { return windDirection; }
            private set { windDirection = value; }
        }
        public int WindSpeed
        {
            get { return windSpeed; }
            private set { windSpeed = value; }
        }

        public Weather(string city)
        {
            UpdateWeather(city);
        }

        private void UpdateWeather(String city)
        {
            XmlDocument xml = Data.HTTPRequest.GetXML("http://www.google.com/ig/api?weather="+city+"&hl="+ Languages.CodeRegion);

            if (xml != null)
            {
                try 
	            {	        
		            XmlNodeList current = xml.ChildNodes.Item(1).ChildNodes.Item(0).ChildNodes.Item(1).ChildNodes;

                    this.condition = current.Item(0).Attributes.GetNamedItem("data").Value;
                    this.temperatureF = int.Parse(current.Item(1).Attributes.GetNamedItem("data").Value);
                    this.temperatureC = int.Parse(current.Item(2).Attributes.GetNamedItem("data").Value);
                    this.humidity = ParseHumidity(current.Item(3).Attributes.GetNamedItem("data").Value);
                    this.windDirection = ParseWindDirection(current.Item(5).Attributes.GetNamedItem("data").Value);
                    this.windSpeed = ParseWindSpeed(current.Item(5).Attributes.GetNamedItem("data").Value);
	            }
	            catch (Exception)
	            {
                    InfoMessages.ErrorMessage("No existe ninguna ciudad con ese nombre");
	            }
            }
        }

        private int ParseHumidity(String text)
        {
            return 50;
        }

        private string ParseWindDirection(String text)
        {
            return "N";
        }

        private int ParseWindSpeed(String text)
        {
            return 12;
        }
    }
}
