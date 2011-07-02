using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using Data;
using System.Timers;

namespace Utils
{
    public class Weather
    {
        public event Action<int> temperatureMaxEvent;
        public event Action<int> temperatureMinEvent;
        private int maxTemperatureEvent;
        private int minTemperatureEvent;
        private String condition;
        private int temperatureF;
        private int temperatureC;
        private int humidity;
        private String windDirection;
        private int windSpeed;
        private Timer timer;
        private string lastCity;

        public Weather()
        {
            //cada 10 min
            timer = new Timer(1000 * 60 * 10);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            lastCity = "";
            timer.Enabled = true;
            GC.KeepAlive(timer);
            maxTemperatureEvent = 29;
            minTemperatureEvent = 16;
        }

        public Weather(string city)
        {
            //cada 10 min
            timer = new Timer(1000 * 60 * 10);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            lastCity = city;
            timer.Enabled = true;
            GC.KeepAlive(timer);
            maxTemperatureEvent = 29;
            minTemperatureEvent = 16;
            UpdateWeather(city);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (lastCity != "")
            {
                UpdateWeather(lastCity);
            }
        }

        public void ForceUpdate()
        {
            UpdateWeather(lastCity);
        }

        public int MaxTemperatureEvent
        {
            get { return maxTemperatureEvent; }
            set { maxTemperatureEvent = value; }
        }

        public int MinTemperatureEvent
        {
            get { return minTemperatureEvent; }
            set { minTemperatureEvent = value; }
        }

        public string City
        {
            get { return lastCity; }
            set { lastCity = value; }
        }

        public void ChangeCity(string city)
        {
            UpdateWeather(city);
        }

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

        private void UpdateWeather(String city)
        {
            XmlDocument xml = Data.HTTPRequest.GetXML("http://www.google.com/ig/api?weather="+city+"&hl="+ Languages.CodeRegion);
            lastCity = city;
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
                    if (temperatureC > maxTemperatureEvent && temperatureMaxEvent != null)
                    {
                        temperatureMaxEvent(temperatureC);
                    }
                    if (temperatureC < minTemperatureEvent && temperatureMinEvent != null)
                    {
                        temperatureMinEvent(temperatureC);
                    }
	            }
	            catch (Exception)
	            {
                    InfoMessages.ErrorMessage("No existe ninguna ciudad con ese nombre");
	            }
            }
        }

        private int ParseHumidity(String text)
        {
            try
            {
                text = text.Split(' ')[1].Split('%')[0];
                return Convert.ToInt32(text);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private string ParseWindDirection(String text)
        {
            try
            {
                text = text.Split(' ')[1];
                return text;
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        private int ParseWindSpeed(String text)
        {
            try
            {
                text = text.Split(' ')[3];
                return Convert.ToInt32(text);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
