using System;
using System.Collections.Generic;
using System.Text;

namespace ClientDB
{
    /// <summary>
    /// This class is used to connect to the DB server, which can interact with the database
    /// </summary>
    public class DBClient : IUnityComponentResetable, IConnectionResultHandler
    {
        private DBClientMessageHandler messageHandler;
        private IClientConfig config;
        private PersistentData data;
        private ILogger logger;
        private IClientDBEventHandler eventHandler;
        private Client client;

        public DBClient(IClientConfig config,IClientDBEventHandler eventHandler,PersistentData data,ILogger logger)
        {
            this.config = config;
            this.data = data;
            this.logger = logger;
            this.eventHandler = eventHandler;
        }

        public void Connect()
        {
            messageHandler = new DBClientMessageHandler(eventHandler,logger);
            client = new Client(this, ClientConnectionInfo.DBConnectionInfo(config), messageHandler, data, logger, this, config);
            messageHandler.Init(client);
        }

        public void Update(float deltaTime)
        {
            if (client != null)
                client.Update(deltaTime);
        }

        public void Send(object msg)
        {
            client.sender.Send(msg);
        }

        public void Clean()
        {
        }

        public void Setup_Succesful()
        {
            eventHandler.ConnectingSuccesful();
        }

        public void Setup_Failed()
        {
            eventHandler.ConnectingFailed();
        }

        public void Setup_ConnectingAttempt(int connectionAttempts)
        {
            eventHandler.ConnectingAttempt(connectionAttempts);
        }
    }
}
