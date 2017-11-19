using Server;
using ServerGameObjectExtension;
using System;
using System.Collections.Generic;

namespace DatabaseConnector
{
    internal class DBMessageHandler : IMessageHandler
    {
        private ILogger logger;
        private DBThread dBThread;
        private MessageCommandHandlerServer commandHandler;

        public DBMessageHandler(ILogger logger, DBThread dBThread)
        {
            this.logger = logger;
            this.dBThread = dBThread;
            commandHandler = new MessageCommandHandlerServer();
        }

        public void Handle(object data, Server_ServerClient client)
        {
            Console.WriteLine("DB - Data type recieved of type " + data.GetType().ToString());
            if (commandHandler.Contains(data.GetType()))
                commandHandler.Execute(data.GetType(), data, client);
        }

        /// <summary>
        /// Setup handlers that requires the ServerCore to be setup
        /// </summary>
        /// <param name="sender"></param>
        public void Init(ServerCore server)
        {
            commandHandler.Add(new MessageHandler_ClientRequest_Login(server));
            commandHandler.Add(new MessageHandler_ClientRequest_Register(server));
        }
    }
}