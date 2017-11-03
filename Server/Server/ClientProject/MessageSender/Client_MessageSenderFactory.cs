using System;

public class Client_MessageSenderFactory
{
    public static IClient_MessageSender CreateSender(IClientConfig config, IClock clock, ClientConnection clientConnection,ILogger logger,Client client)
    {
        if (config.GetInt("MessageSender_FakeDelayInMiliSeconds") == 0)
            return new Client_MessageSender(clientConnection, logger);
        else
            return new Client_MessageSender_FakeDelay(config.GetInt("MessageSender_FakeDelayInMiliSeconds"), clock, clientConnection, logger,client);
    }
}