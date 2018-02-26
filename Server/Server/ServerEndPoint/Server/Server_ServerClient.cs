using System;
using System.Collections.Generic;
using System.Net.Sockets;

/// <summary>
/// The client representation on the server
/// This holds both information about stats and network connection
/// </summary>
public class Server_ServerClient
{
    public Shared_PlayerInfo info;
    public TcpClient tcp;
    public int DBPlayerId;
    public int kills;
    public int deaths;

    double? cachedPing;
    List<double> pingsRecorded;
    double lastPingTimeStamp;
    static int totalPingsRecordedPerPlayer = 5;
    
    /// <summary>
    /// Representation on server of a client, instead of having a tcpClient, this class allows for some context
    /// </summary>
    /// <param name="tcp"></param>
    public Server_ServerClient(TcpClient tcp)
    {
        this.tcp = tcp;
        pingsRecorded = new List<double>();
    }

    public void Register(Shared_PlayerInfo info)
    {
        this.info = info;
    }

    /// <summary>
    /// Returns the ping
    /// Calculates it if no cached values is found
    /// </summary>
    /// <returns>The ping of a player</returns>
    public double GetPingInMiliSeconds()
    {
        if (!cachedPing.HasValue)
        {
            cachedPing = CalculatePing();
        }
        return cachedPing.Value;
    }

    /// <summary>
    /// Calculates the ping by averaging the recorded pings
    /// </summary>
    /// <returns></returns>
    private double? CalculatePing()
    {
        double average = 0;
        foreach (var ping in pingsRecorded)
        {
            average += ping;
        }
        return average / pingsRecorded.Count;
    }

    /// <summary>
    /// Update the ping of the player with an updated value
    /// </summary>
    /// <param name="pingUpdate"></param>
    public void UpdatePing(double pingUpdate, double clockTimeStamp)
    {
        lastPingTimeStamp = clockTimeStamp;
        pingsRecorded.Add(pingUpdate);
        if(totalPingsRecordedPerPlayer < pingsRecorded.Count)
        {
            pingsRecorded.RemoveAt(0);
        }
        cachedPing = null;
    }

    public bool HasSufficientPingsRecorded()
    {
        return pingsRecorded.Count >= totalPingsRecordedPerPlayer; 
    }

    public double GetLastPingTimeStamp()
    {
        if (pingsRecorded.Count == 0)
            return double.PositiveInfinity;
        else
            return lastPingTimeStamp;
    }
}