using System;
using System.Collections.Generic;

/// <summary>
/// Commandhandler allowing for more easily managing and isolating commands
/// This is used by messagehandlers to administrate messages and their handles
/// </summary>
public class MessageCommandHandlerServer
{
    Dictionary<Type, IMessageHandlerCommand> commands;

    public MessageCommandHandlerServer()
    {
        commands = new Dictionary<Type, IMessageHandlerCommand>();
    }
    public void Add(IMessageHandlerCommand cmd)
    {
        commands.Add(cmd.GetMessageTypeSupported(), cmd);
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