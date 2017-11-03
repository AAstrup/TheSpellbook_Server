public interface IClock
{
    /// <summary>
    /// Expects time in a match as given by the server
    /// </summary>
    /// <returns></returns>
    double GetTimeInMiliSeconds();
}