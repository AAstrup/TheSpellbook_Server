public interface IClientConfig
{
    int GetInt(string key);
    string GetString(string key);

    /*
     * Keys expected with default values
    <add key="IpOfMatch" value="127.0.0.1"></add>
    <add key="IpOfMatchMaker" value="127.0.0.1"></add>
    <add key="PortOfMatchMaker" value="61497"></add>
     */
}