using System;
using System.Configuration;
using System.Net;
/// <summary>
/// Static values are kept here
/// This will be replaced with a txt at a later point
/// </summary>
namespace Server
{
    public class ServerConfig
    {
        //Free port https://www.speedguide.net/port.php?port=61497
        //public static int PortOfMatchMaker = 61497;
        public static string LocalIp = "127.0.0.1";
        public static int FirstPortOfMatches = 61498;//Will increment as more matches are created
        public static IPAddress IpAddressOfMatchMaker = IPAddress.Any;
        public static IPAddress IpAddressLocal = IPAddress.Any;

        public static int GetInt(string key)
        {
            string val = ConfigurationManager.AppSettings[key];
            if (val == null)
                throw new Exception("Appsetting for key '" + key + "' is missing, none were found or it was empty");
            return Int32.Parse(val);
        }

        public static bool GetBool(string key)
        {
            string val = ConfigurationManager.AppSettings[key];
            if (val == null)
                throw new Exception("Appsetting for key '" + key + "' is missing, none were found or it was empty");
            return val.Equals("true");
        }
        //public static int PlayerCountInAMatch = 2;
    }
}