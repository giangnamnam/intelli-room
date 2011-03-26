using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliRoom;

namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            Language lang = new Spanish();
            while (true)
            {
                Console.Out.WriteLine(lang.GetText(Console.In.ReadLine()));
            }
        }
    }
}
