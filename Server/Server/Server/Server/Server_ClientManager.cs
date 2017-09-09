using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the clients of the server
/// </summary>
public class Server_ClientManager  {
    private List<Server_ServerClient> clients;
    private List<Server_ServerClient> disconnects;
    private ServerCore server;

    public Server_ClientManager(ServerCore server)
    {
        this.server = server;
        clients = new List<Server_ServerClient>();
        disconnects = new List<Server_ServerClient>();
    }

    public void AddClient(Server_ServerClient sc)
    {
        clients.Add(sc);
    }

    public List<Server_ServerClient> GetClients()
    {
        return clients;
    }

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
}

