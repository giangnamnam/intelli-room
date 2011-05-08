using System;

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
            //Llamo al Init de comando, solo deberia llamarse una vez
            IntelliRoom.Command.Init();
            //devuelvo el texto de entrada
            return "Wellcome to IntelliRoom\n";
        }
    }
}
