using System;

internal class Handler_Response_Ping : IMessageHandlerCommandClient
{
    private IMatchEventHandler matchEventHandler;
    private Client_MessageSender sender;

    public Handler_Response_Ping(IMatchEventHandler matchEventHandler, Client_MessageSender sender)
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