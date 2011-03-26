using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliRoom;

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

        public static String Start()
        {
            return "Wellcome to IntelliRoom\n";
        }

        
    }
}
