using System;

namespace ClientDB
{
    public class MessageHandler_ServerResponse_Register : IMessageHandlerCommandClient
    {
        private IClientDBEventHandler eventHandler;

        public MessageHandler_ServerResponse_Register(IClientDBEventHandler eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ServerResponse_Register);
        }

        public void Handle(object objData)
        {
            eventHandler.RegisterAndLoginResponse((Message_ServerResponse_Register)objData);
        }
    }
}