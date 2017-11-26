using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the clients of the server
/// </summary>
public class Server_ClientManager  {
    private List<Server_ServerClient> clients;
    Dictionary<int, Server_ServerClient> GUIDToClient;
    private List<Server_ServerClient> disconnects;
    private ServerCore server;

    public Server_ClientManager(ServerCore server)
    {
        this.server = server;
        GUIDToClient = new Dictionary<int, Server_ServerClient>();
        clients = new List<Server_ServerClient>();
        disconnects = new List<Server_ServerClient>();
    }

    /// <summary>
    /// Called by client through MessageHandler_Request_JoinQueue to register 
    /// Registering the client with their info
    /// </summary>
    /// <param name="client"></param>
    public void RegisterClient(Server_ServerClient client)
    {
        GUIDToClient.Add(client.info.GUID, client);
    }

    /// <summary>
    /// Registering the client without their info
    /// </summary>
    /// <param name="sc"></param>
    public void AddClient(Server_ServerClient sc)
    {
        clients.Add(sc);
    }

    public List<Server_ServerClient> GetClients()
    {
        return clients;
    }

    /// <summary>
    /// When a client leaves this is called
    /// This includes when a client is not reachable
    /// </summary>
    /// <param name="server_ServerClient"></param>
    internal void ClientLeft(Server_ServerClient server_ServerClient)
    {
        clients.Remove(server_ServerClient);
        disconnects.Add(server_ServerClient);
        server.eventHandler.ClientLeft(server_ServerClient,server);
    }

    /// <summary>
    /// Update a client with player information
    /// </summary>
    /// <param name="playerInfo">New information</param>
    /// <param name="clientToUpdate">Player to update</param>
    public void UpdatePlayerInfo(Shared_PlayerInfo playerInfo, Server_ServerClient clientToUpdate)
    {
        foreach (var client in clients)
        {
            if (client == clientToUpdate)
            {
                client.info = playerInfo;
                return;
            }
        }
    }

    public Server_ServerClient GetClient(int GUID)
    {
        return GUIDToClient[GUID];
    }

    /// <summary>
    /// Remove a client from the list
    /// </summary>
    /// <param name="client"></param>
    public void RemoveClient(Server_ServerClient client)
    {
        clients.Remove(client);
    }
}

