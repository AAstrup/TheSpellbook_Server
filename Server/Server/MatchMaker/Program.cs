using System;
using System.Collections.Generic;
using System.Text;

namespace MatchMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Server_MessageHandler handler = new Server_MessageHandler();
            ServerCore serverCore = new ServerCore(handler,ConnectionInfo.MatchMakerConnectionInfo());
            handler.Setup(serverCore);
            ILogger logger = new ConsoleLogger();
            MatchMakerCore matchMaker = new MatchMakerCore(logger,serverCore.messageSender);

            Console.WriteLine("Server started!");
            Console.WriteLine("Press Q to stop");
            Console.WriteLine("Assemblies" + AppDomain.CurrentDomain.GetAssemblies());

            while (true)
            {
                Console.Write(".");
                serverCore.Update();
                matchMaker.Update(serverCore.clientManager.GetClients());
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
