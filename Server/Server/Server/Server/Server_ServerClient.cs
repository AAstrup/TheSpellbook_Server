using System;
using System.Net.Sockets;

/// <summary>
/// The client representation on the server
/// This holds both information about stats and network connection
/// </summary>
public class Server_ServerClient
{
    public Shared_PlayerInfo info;
    public TcpClient tcp;
    /// <summary>
    /// Representation on server for a client, instead of having a tcpClient, this class allows for some context
    /// </summary>
    /// <param name="tcp"></param>
    public Server_ServerClient(TcpClient tcp)
    {
        this.tcp = tcp;
    }
    public void Register(Shared_PlayerInfo info)
    {
        this.info = info;
    }
}