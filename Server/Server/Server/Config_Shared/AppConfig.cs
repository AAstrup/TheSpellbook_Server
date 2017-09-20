using System.Net;
/// <summary>
/// Static values are kept here
/// This will be replaced with a txt at a later point
/// </summary>
namespace Server
{
    public class AppConfig
    {
        //Free port https://www.speedguide.net/port.php?port=61497
        public static int PortOfMatchMaker = 61497;
        public static string IpOfMatch = "127.0.0.1";
        public static string LocalIp = "127.0.0.1";
        public static int FirstPortOfMatches = 61498;//Will increment as more matches are created
        public static IPAddress IpAddressOfMatchMaker = IPAddress.Any;
        public static IPAddress IpAddressLocal = IPAddress.Any;
    }
}