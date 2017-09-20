using System;
using System.Collections.Generic;

/// <summary>
/// Commandhandler allowing for more easily managing and isolating commands
/// This is used by messagehandlers to administrate messages and their handles
/// </summary>
public class MessageCommandHandler
{
    Dictionary<Type, IMessageHandlerCommand> commands;
    public MessageCommandHandler()
    {
        commands = new Dictionary<Type, IMessageHandlerCommand>();
    }
    public void Add(Type msgType,IMessageHandlerCommand cmd)
    {
        commands.Add(msgType,cmd);
    }

    public void Execute(Type msgType, object data,Server_ServerClient client)
    {
        commands[msgType].Handle(data, client);
    }

    public bool Contains(Type type)
    {
        return commands.ContainsKey(type);
    }
}