using System;
using System.Collections.Generic;
using ServerGameObjectExtension;
using Server;

namespace Match
{
    internal class MatchExtensionFactory : List<IServerExtension>
    {
        internal static List<IServerExtension> CreateExtensions(ILogger logger)
        {
            var list = new List<IServerExtension>();
            if (ServerConfig.GetBool("ServerGameObjectExtensionWrapper"))
            {
                list.Add(new ServerGameObjectExtensionWrapper());
                logger.Log("Running with ServerGameObjectExtensionWrapper");
            }
            return list;
        }
    }
}