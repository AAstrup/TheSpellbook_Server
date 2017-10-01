using System;
using System.Collections.Generic;

public class MessageCommandHandlerClient
{
    Dictionary<Type, IMessageHandlerCommandClient> commands;
    public MessageCommandHandlerClient()
    {
        commands = new Dictionary<Type, IMessageHandlerCommandClient>();
    }
    public void Add(Type msgType, IMessageHandlerCommandClient cmd)
    {
        commands.Add(msgType, cmd);
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