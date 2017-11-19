using System;

namespace ClientDB
{
    public class MessageHandler_ServerResponse_Login : IMessageHandlerCommandClient
    {
        private IClientDBEventHandler eventHandler;

        public MessageHandler_ServerResponse_Login(IClientDBEventHandler eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ServerResponse_Login);
        }

        public void Handle(object objData)
        {
            eventHandler.LoginResponse((Message_ServerResponse_Login)objData);
        }
    }
}