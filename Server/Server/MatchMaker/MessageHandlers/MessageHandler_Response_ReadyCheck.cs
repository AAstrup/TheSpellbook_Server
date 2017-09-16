using MatchMaker;
using System;
using System.Collections.Generic;

internal class MessageHandler_Response_ReadyCheck : IMessageHandlerCommand
{
    private ServerCore server;
    Dictionary<int, MatchReadyQueue> readyCheckGuidToMatchReadyQueue;
    List<MatchReadyQueue> queuesRunningSorted;
    private MatchMakerCore matchMakerCore;

    public MessageHandler_Response_ReadyCheck(MatchMakerCore matchMakerCore,ServerCore server)
    {
        this.server = server;
        readyCheckGuidToMatchReadyQueue = new Dictionary<int, MatchReadyQueue>();
        queuesRunningSorted = new List<MatchReadyQueue>();
        this.matchMakerCore = matchMakerCore;
        matchMakerCore.matchReadyCheckInitiated += CreateNewReadyQueue;
    }

    /// <summary>
    /// Create new ready queue
    /// </summary>
    /// <param name="readyCheck">The ready message that was sent to the clients</param>
    /// <param name="p1">Player 1</param>
    /// <param name="p2">Player 2</param>
    public void CreateNewReadyQueue(Server_ServerClient p1, Server_ServerClient p2, Message_ServerRequest_ReadyCheck readyCheck)
    {
        Console.WriteLine("Creating new ready queue");
        MatchReadyQueue matchQueue = new MatchReadyQueue(new List<int>() { p1.info.GUID, p2.info.GUID });
        readyCheckGuidToMatchReadyQueue.Add(readyCheck.ReadGUID(),matchQueue);
        queuesRunningSorted.Add(matchQueue);
    }

    /// <summary>
    /// Recieved from players who has checked that they are ready
    /// </summary>
    /// <param name="data">The message sent from the client</param>
    /// <param name="client">The client</param>
    /// <param name="server">The server</param>
    public void Handle(object objData, Server_ServerClient client)
    {
        Message_ClientResponse_ReadyCheck data = (Message_ClientResponse_ReadyCheck)objData;
        var queue = readyCheckGuidToMatchReadyQueue[data.readyCheckGUID_FromServerReadyCheck];
        queue.playersConnected.Add(client.info.GUID);
        Console.WriteLine("Recieved Message_ClientResponse_ReadyCheck!");
        if (queue.playersConnected.Count == queue.playerGuidsToConnect.Count)
        {
            Console.WriteLine("Creating a MATCH");
            matchMakerCore.MakeMatch(server.clientManager.GetClient(queue.playersConnected[0]), server.clientManager.GetClient(queue.playersConnected[1]));
        }
    }

    private class MatchReadyQueue{
        public List<int> playerGuidsToConnect;
        public List<int> playersConnected;
        public DateTime timeEnd;

        public MatchReadyQueue(List<int> playerGuidsToConnect)
        {
            this.playerGuidsToConnect = playerGuidsToConnect;
            playersConnected = new List<int>();
            timeEnd = DateTime.Now;
            timeEnd.Add(MatchMakerConfig.MatchReadyQueueTime);
        }
    }
}