using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

internal class Client_MessageSender_FakeDelay : IClient_MessageSender
{
    private IClock clock;
    List<FakeDelayMessage> messagesToSend;
    private ClientConnection connection;
    ILogger logger;
    public Client_MessageSender_FakeDelay(int delay,IClock clock, ClientConnection connection, ILogger logger,Client client)
    {
        client.ClockUpdateEvent += Update;
        this.clock = clock;
        messagesToSend = new List<FakeDelayMessage>();
        this.logger = logger;
        this.connection = connection;
    }

    /// <summary>
    /// Stores a serialized message to be send later after the delay
    /// </summary>
    /// <param name="serializable">The serializable object being send</param>
    public void Send(object serializable)
    {
        if (!connection.SocketIsReady())
        {
            logger.Log("Socket not ready, message not set!");
            return;
        }
        messagesToSend.Add(new FakeDelayMessage(clock.GetTimeInMiliSeconds(), serializable));
    }

    /// <summary>
    /// Register the client at the server with the player information
    /// </summary>
    /// <param name="playerInfo"></param>
    public void RegisterAtServer(Shared_PlayerInfo playerInfo)
    {
        var msg = new Message_Request_JoinQueue(playerInfo);
        Send(msg);
    }

    /// <summary>
    /// Updates by checking if messages has passed the time of the delay and then sends it
    /// </summary>
    public void Update(double clockTime)
    {
        while(messagesToSend.Count > 0 && clock.GetTimeInMiliSeconds() > messagesToSend[0].timeToSend)
        {
            BinaryFormatter form = new BinaryFormatter();
            form.Serialize(connection.GetSocket().GetStream(), messagesToSend[0].msg);
            messagesToSend.RemoveAt(0);
        }
    }

    class FakeDelayMessage
    {
        public double timeToSend;
        public object msg;

        public FakeDelayMessage(double timeToSend, object msg)
        {
            this.timeToSend = timeToSend;
            this.msg = msg;
        }
    }
}