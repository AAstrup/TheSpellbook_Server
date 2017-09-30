using MatchMaker;
using Server;
using System;
using System.Collections.Generic;

internal class MessageHandler_Response_ReadyCheck : IMessageHandlerCommand
{
    private ServerCore server;
    Dictionary<int, MatchReadyQueue> readyCheckGuidToMatchReadyQueue;
    List<MatchReadyQueue> queuesRunningSorted;
    private MatchMakerCore matchMakerCore;

    public MessageHandler_Response_ReadyCheck(MatchMakerCore matchMakerCore, ServerCore server)
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
    /// <param name="clients">Clients joining the ready queue</param>
    public void CreateNewReadyQueue(List<Server_ServerClient> clients, Message_ServerRequest_ReadyCheck readyCheck)
    {
        Console.WriteLine("Creating new ready queue");
        MatchReadyQueue matchQueue = new MatchReadyQueue();
        readyCheckGuidToMatchReadyQueue.Add(readyCheck.ReadGUID(), matchQueue);
        queuesRunningSorted.Add(matchQueue);
    }

    /// <summary>
    /// Recieved from players who has checked that they are ready
    /// </summary>
    /// <param name="data">The message sent from the client</param>
    /// <param name="client">The client connecting</param>
    /// <param name="server">The server</param>
    public void Handle(object objData, Server_ServerClient client)
    {
        Message_ClientResponse_ReadyCheck data = (Message_ClientResponse_ReadyCheck)objData;
        var queue = readyCheckGuidToMatchReadyQueue[data.readyCheckGUID_FromServerReadyCheck];
        queue.playersConnected.Add(client.info.GUID);
        Console.WriteLine("Recieved Message_ClientResponse_ReadyCheck!");
        if (queue.playersConnected.Count == AppConfig.PlayerCountInAMatch)
        {
            Console.WriteLine("Creating a MATCH");
            List<Server_ServerClient> toJoinMatch = new List<Server_ServerClient>(AppConfig.PlayerCountInAMatch);

            while (toJoinMatch.Count < AppConfig.PlayerCountInAMatch)
            {
                var player = server.clientManager.GetClients()[0];
                server.clientManager.GetClients().RemoveAt(0);
                toJoinMatch.Add(player);
            }

            matchMakerCore.MakeMatch(toJoinMatch);//server.clientManager.GetClient(queue.playersConnected[0]), server.clientManager.GetClient(queue.playersConnected[1]));
        }
    }

    private class MatchReadyQueue
    {
        public List<int> playersConnected;
        public DateTime timeEnd;

        public MatchReadyQueue()
        {
            playersConnected = new List<int>();
            timeEnd = DateTime.Now;
            timeEnd.Add(MatchMakerConfig.MatchReadyQueueTime);
        }
    }
}