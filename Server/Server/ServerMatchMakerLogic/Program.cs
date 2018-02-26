using DatabaseConnector;
using Match;
using Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MatchMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Server_MessageHandler handler = new Server_MessageHandler();
            ILogger logger = new ConsoleLogger();
            ServerCore serverCore = new ServerCore(handler,ServerConnectionInfo.MatchMakerConnectionInfo(),logger);
            MatchMakerCore matchMaker = new MatchMakerCore(serverCore,logger, serverCore.messageSender);
            handler.Setup(serverCore,matchMaker);
            int tickrate = 33;
            int secondToWait = 1000 / tickrate;
            Console.WriteLine("Server started!");

            var DBThread = new DBThread(logger);
            Thread matchThread = new Thread(new ThreadStart(DBThread.ThreadStart));
            matchThread.Start();

            while (true)
            {
                serverCore.Update();
                matchMaker.Update();
                System.Threading.Thread.Sleep(secondToWait);
            }
        }
    }
}
