using System;
using System.Collections.Generic;

/// <summary>
/// Command pattern for message type to the respectaple handler
/// </summary>
public class MessageCommandHandlerClient
{
    Dictionary<Type, IMessageHandlerCommandClient> commands;

    public MessageCommandHandlerClient()
    {
        commands = new Dictionary<Type, IMessageHandlerCommandClient>();
    }

    public MessageCommandHandlerClient(Dictionary<Type, IMessageHandlerCommandClient> msgHandler)
    {
        commands = msgHandler;
    }

    public void Add( IMessageHandlerCommandClient cmd)
    {
        commands.Add(cmd.GetMessageTypeSupported(), cmd);
    }

    public void Execute(Type msgType, object data)
    {
        commands[msgType].Handle(data);
    }

    public bool Contains(Type type)
    {
        return commands.ContainsKey(type);
    }
}