using System;
using System.Collections.Generic;
using System.Text;

namespace MatchMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            MatchMakerCore matchMaker = new MatchMakerCore();

            Server_MessageHandler handler = new Server_MessageHandler();
            ServerCore serverCore = new ServerCore(handler);
            handler.Setup(serverCore);

            Console.WriteLine("Server started!");
            Console.WriteLine("Press Q to stop");
            Console.WriteLine("Assemblies" + AppDomain.CurrentDomain.GetAssemblies());

            while (true)
            {
                Console.WriteLine("Updating ...");
                serverCore.Update();
                matchMaker.Update(serverCore.clientManager.GetClients());
                System.Threading.Thread.Sleep(500);
                //string key = Console.ReadKey(true).Key.ToString();
                //Console.WriteLine();
                //if (key.ToUpper() == "Q")
                //    break;
            }

            Console.WriteLine("Server stopped!");
            Console.WriteLine("Press any key to continue...");
            Console.WriteLine(Console.ReadKey());
        }
    }
}
