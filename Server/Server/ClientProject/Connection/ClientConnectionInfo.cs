using System;
using System.Configuration;
using System.Net;

/// <summary>
/// Class contains connection information for setting up a match
/// Send to client when creating a match so that they can connect to the match
/// </summary>
public class ClientConnectionInfo
{
    public string Ip;
    public int Port;
    public ClientConnectionInfo(int Port, string Ip)
    {
        this.Port = Port;
        this.Ip = Ip;
    }

    public static ClientConnectionInfo DBConnectionInfo(IClientConfig config)
    {
        return new ClientConnectionInfo(
            config.GetInt("PortOfDB"),
            config.GetString("IpOfDB"));
    }

    public static ClientConnectionInfo MatchMakerConnectionInfo(IClientConfig config)
    {
        return new ClientConnectionInfo(
            config.GetInt("PortOfMatchMaker"),
            config.GetString("IpOfMatchMaker"));
    }

    private static ClientConnectionInfo LocalIp(IClientConfig config)
    {
        throw new NotImplementedException();
    }
}
