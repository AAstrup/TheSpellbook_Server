using System.Net;

/// <summary>
/// Class contains connection information for setting up a match
/// Send to client when creating a match so that they can connect to the match
/// </summary>
public class ConnectionInfo
{
    public IPAddress Ip;
    public int Port;
    public ConnectionInfo(int Port, IPAddress Ip)
    {
        this.Port = Port;
        this.Ip = Ip;
    }
    public ConnectionInfo(int Port)
    {
        this.Port = Port;
        Ip = IPAddress.Any;
    }
    public static ConnectionInfo MatchMakerConnectionInfo()
    {
        return new ConnectionInfo(AppConfig.PortOfMatchMaker);
    }
}