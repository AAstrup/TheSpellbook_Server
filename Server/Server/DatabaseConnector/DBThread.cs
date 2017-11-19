using Server;

namespace DatabaseConnector
{
    public class DBThread
    {
        private ILogger logger;
        private ServerCore server;
        private int miliSecondPerTick;

        public DBThread(ILogger logger)
        {
            this.logger = logger;
            miliSecondPerTick = 1000 / ServerConfig.GetInt("DBTickRate");
        }

        /// <summary>
        /// Only call this method on an another thread 
        /// Starts the thread running the match
        /// </summary>
        public void ThreadStart()
        {
            var DBHandler = new DBMessageHandler(logger, this);
            server = new ServerCore(DBHandler, ServerConnectionInfo.DBConnectionInfo(), logger);
            DBHandler.Init(server);
            logger.Log("Database setup and running");

            while (true)
            {
                server.Update();
                System.Threading.Thread.Sleep(miliSecondPerTick);
            }
        }
    }
}