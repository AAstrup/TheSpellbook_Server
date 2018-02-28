using System;
using System.Collections.Generic;
using Server;
using ServerExtensionTools;
using ServerGameObjectExtension;

namespace Match
{
    internal class MatchExtensionFactory : List<IServerExtension>
    {
        internal static List<IServerExtension> CreateExtensions(ILogger logger)
        {
            var list = new List<IServerExtension>();
            if (ServerConfig.GetBool("SharedClientServerGameObjectMessagesWrapper"))
            {
                list.Add(new SharedClientServerGameObjectMessagesWrapper());
                logger.Log("Running with SharedClientServerGameObjectMessagesWrapper");
            }
            return list;
        }
    }
}