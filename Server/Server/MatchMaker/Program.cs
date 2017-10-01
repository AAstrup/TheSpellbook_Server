using Match;
using Server;
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
            ServerCore serverCore = new ServerCore(handler,ServerConnectionInfo.MatchMakerConnectionInfo());
            ILogger logger = new ConsoleLogger();
            MatchMakerCore matchMaker = new MatchMakerCore(serverCore,logger, serverCore.messageSender);
            handler.Setup(serverCore,matchMaker);
            int tickrate = 33;
            int secondToWait = 1000 / tickrate;
            Console.WriteLine("Server started!");
            Console.WriteLine("Press Q to stop");
            Console.WriteLine("Assemblies" + AppDomain.CurrentDomain.GetAssemblies());

            while (true)
            {
                serverCore.Update();
                matchMaker.Update();
                System.Threading.Thread.Sleep(secondToWait);
            }
        }
    }
}
