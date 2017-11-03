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

    private IConnectionResultHandler handler;
    public float CurrentConnectionSeconds;
    private int connectionAttempts;
    private string host;
    private int port;

    public ClientConnection(ClientConnectionInfo connectionInfo, ILogger logger, IConnectionResultHandler handler)
    {
        this.handler = handler;
        this.logger = logger;
        string host = connectionInfo.Ip;
        int port = connectionInfo.Port;
        logger.Log("Connecting created");
        ConnectToServer(host, port);
    }

    public NetworkStream GetStream()
    {
        return stream;
    }

    private void ConnectToServer(string host, int port)
    {
        if (socketReady)
            return;
        this.host = host;
        this.port = port;
        connectionAttempts = 0;
        CurrentConnectionSeconds = 0f;
        logger.Log("Connecting started");
    }

    public void Update(float deltaTime)
    {
        if (socketReady)
            return;
        CurrentConnectionSeconds += deltaTime;
        logger.Log("Connecting updated, seconds " + CurrentConnectionSeconds + " total time waiting for " + NonUserClientConfig.ConnectionWaitTimeSeconds
            + ". connection attempt " + connectionAttempts + " out of " + NonUserClientConfig.maxConnectionAttempts);

        if (CurrentConnectionSeconds < NonUserClientConfig.ConnectionWaitTimeSeconds && connectionAttempts <= NonUserClientConfig.maxConnectionAttempts)
        {
            CurrentConnectionSeconds = 0f;
            ConnectAttempt();
        }
    }

    public void ConnectAttempt()
    {
        connectionAttempts++;
        handler.Setup_ConnectingAttempt(connectionAttempts);
        logger.Log("Connecting attempt" + connectionAttempts);

        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            handler.Setup_Succesful();
        }
        catch (Exception e)
        {
            logger.Log(e.Message);
        }
        if (connectionAttempts > NonUserClientConfig.maxConnectionAttempts)
        {
            handler.Setup_Failed();
        }
    }

    public TcpClient GetSocket()
    {
        return socket;
    }

    public bool SocketIsReady() { return socketReady; }
}