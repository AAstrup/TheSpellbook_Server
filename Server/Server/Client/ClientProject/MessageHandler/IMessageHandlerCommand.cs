using System;

public interface IMessageHandlerCommandClient
{
    /// <summary>
    /// Get the type of the message that this handler will be called to handle
    /// </summary>
    /// <returns>Type of message this handler will handle</returns>
    Type GetMessageTypeSupported();
    /// <summary>
    /// Handle the message
    /// </summary>
    /// <param name="objData"></param>
    void Handle(object objData);
}