namespace ClientDB
{
    internal class DBClientMessageHandler : IMessageHandler
    {
        private ILogger logger;
        private IClientDBEventHandler eventHandler;
        private MessageCommandHandlerClient commandHandler;
        private Client client;

        public DBClientMessageHandler(IClientDBEventHandler eventHandler,ILogger logger)
        {
            this.logger = logger;
            this.eventHandler = eventHandler;
            commandHandler = new MessageCommandHandlerClient();

        }

        public void Init(Client client)
        {
            this.client = client;

            commandHandler.Add(new MessageHandler_ServerResponse_Login(eventHandler));
            commandHandler.Add(new MessageHandler_ServerResponse_Register(eventHandler));
        }

        public void Handle(object data)
        {
            if (commandHandler.Contains(data.GetType()))
                commandHandler.Execute(data.GetType(), data);
            else
            {
                logger.Log("Data type UKNOWN! Type: " + data.GetType().ToString());
            }
        }
    }
}