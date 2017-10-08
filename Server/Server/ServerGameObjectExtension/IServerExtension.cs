namespace ServerGameObjectExtension
{
    public interface IServerExtension
    {
        /// <summary>
        /// Get message requried to setup a client, if any
        /// Returns null if nothing has to be send
        /// </summary>
        /// <param name="client"></param>
        object GetMessageForClientSetup(Server_ServerClient client);
    }
}