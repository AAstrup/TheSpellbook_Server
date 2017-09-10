using Match;

namespace MatchMaker
{
    /// <summary>
    /// Used to write to console from projects that build as class libraries
    /// </summary>
    internal class ConsoleLogger : ILogger
    {
        public void Log(string s)
        {
            System.Console.WriteLine("Logger - "+s);
        }
    }
}