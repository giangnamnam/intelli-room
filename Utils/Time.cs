using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Utils
{
    public class Time
    {
        public static String GetStringTime()
        {
            DateTime date = DateTime.Now;
            return "Son las " + date.Hour + " horas y " + date.Minute + " minutos";
        }

        public static String GetStringDate()
        {
            DateTime date = DateTime.Now;
            return "Estamos a " + date.Day + " del mes" + date.Month + " del año " + date.Year;
        }

        public static DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
