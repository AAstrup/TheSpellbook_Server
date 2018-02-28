using Server;
using System.Net;

/// <summary>
/// Class contains connection information for setting up a match
/// Send to client when creating a match so that they can connect to the match
/// </summary>
namespace Server
{
    public class ServerConnectionInfo
    {
        public IPAddress Ip;
        public int Port;
        public ServerConnectionInfo(int Port, IPAddress Ip)
        {
            this.Port = Port;
            this.Ip = Ip;
        }
        public ServerConnectionInfo(int Port)
        {
            this.Port = Port;
            Ip = IPAddress.Any;
        }
        public static ServerConnectionInfo MatchMakerConnectionInfo()
        {
            return new ServerConnectionInfo(ServerConfig.GetInt("PortOfMatchMaker"), ServerConfig.IpAddressOfMatchMaker);
        }
        public static ServerConnectionInfo DBConnectionInfo()
        {
            return new ServerConnectionInfo(ServerConfig.GetInt("PortOfDB"), ServerConfig.IpAddressOfMatchMaker);
        }
        public static ServerConnectionInfo LocalConnectionInfo()
        {
            return new ServerConnectionInfo(ServerConfig.GetInt("PortOfMatchMaker"), ServerConfig.IpAddressLocal);
        }
    }
}