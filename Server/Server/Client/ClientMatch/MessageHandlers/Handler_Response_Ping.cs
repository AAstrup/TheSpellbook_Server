using System;

internal class Handler_Response_Ping : IMessageHandlerCommandClient
{
    private IMatchEventHandler matchEventHandler;
    private IClient_MessageSender sender;

    public Handler_Response_Ping(IMatchEventHandler matchEventHandler, IClient_MessageSender sender)
    {
        this.matchEventHandler = matchEventHandler;
        this.sender = sender;
    }

    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerRoundTrip_Ping);
    }

    public void Handle(object objData)
    {
        sender.Send(objData);
    }
}