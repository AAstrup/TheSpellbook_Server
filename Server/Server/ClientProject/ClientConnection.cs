using System;
using System.IO;
using System.Net.Sockets;

/// <summary>
/// The class responsible for opening a connection the server
/// </summary>
public class ClientConnection
{
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    private bool socketReady;
    private ILogger logger;

    public ClientConnection(ClientConnectionInfo connectionInfo, ILogger logger)
    {
        this.logger = logger;
        string host = connectionInfo.Ip;
        int port = connectionInfo.Port;

        ConnectToServer(host, port);
    }

    public NetworkStream GetStream()
    {
        return stream;
    }

    private bool ConnectToServer(string host, int port)
    {
        if (socketReady)
            return true;
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            socketReady = true;
        }
        catch (Exception e) { logger.Log(e.Message); }
        return socketReady;
    }

    public TcpClient GetSocket()
    {
        return socket;
    }

    public bool SocketIsReady() { return socketReady; }
}