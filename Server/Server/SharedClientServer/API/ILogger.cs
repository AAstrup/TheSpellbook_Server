public interface ILogger
{
    void Log(string s);
    /// <summary>
    /// Less important events, usefull for debugging to know the state of the server
    /// </summary>
    /// <param name="v"></param>
    void DebugLog(string v);
}
