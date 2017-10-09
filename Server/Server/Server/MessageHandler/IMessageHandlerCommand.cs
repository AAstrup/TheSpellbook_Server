using System;
/// <summary>
/// Command pattern for message handlers
/// </summary>
public interface IMessageHandlerCommand
{
    /// <summary>
    /// Get the type of the message that this handler will be called to handle
    /// </summary>
    /// <returns>Type of message this handler will handle</returns>
    Type GetMessageTypeSupported();
    /// <summary>
    /// Called to handle messages
    /// </summary>
    /// <param name="data">Message that must be casted to the supported type</param>
    /// <param name="client">Client that send the msg</param>
    void Handle(object objData, Server_ServerClient client);
}