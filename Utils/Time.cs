﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Utils
{
    public class Time
    {
        public static String GetTime()
        {
            DateTime date = DateTime.Now;
            return "son las " + date.Hour + " horas y " + date.Minute + " minutos";
        }

        public static String GetDate()
        {
            DateTime date = DateTime.Now;
            return "hoy estamos a " + date.Day + " del " + date.Month + " de " + date.Year;
        }
    }
}
