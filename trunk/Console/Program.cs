﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliRoom;
using System.Drawing;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(Start());
            CommandInterpreter commInter = new CommandInterpreter();
            
            while (true)
            {
                System.Console.Write(">: ");
                String command = System.Console.ReadLine();
                System.Console.WriteLine(commInter.Interpreter(command));
                System.Console.WriteLine("-------------------------------------------------");
                
            }

        }

        public static void PrintInformation(String information)
        {
            System.Console.BackgroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("Info: " + information);
            System.Console.BackgroundColor = ConsoleColor.White;
        }
        

        public static String Start()
        {
            //Llamo al Init de comando, solo deberia llamarse una vez
            IntelliRoom.Command.Init();
            //devuelvo el texto de entrada
            return "Wellcome to IntelliRoom\n";
        }

        
    }
}
