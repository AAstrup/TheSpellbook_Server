using System;
using System.Collections.Generic;
using Server;
using ClientServerSharedGameObjectMessages;
using Match;

namespace ServerGameObjectExtension
{
    internal class MessageHandler_ClientRequest_RoundEnded : IMessageHandlerCommand
    {
        Dictionary<Server_ServerClient,PlayerScore> playerToScore;
        List<PlayerScore> scores;
        private PingDeterminer pingDeterminer;
        private Clock matchClock;
        private ServerCore server;
        int round;
        int maxRounds;
        public MessageHandler_ClientRequest_RoundEnded(ServerCore server, MatchGameEventContainer matchGameEventWrapper,Clock matchClock, PingDeterminer pingDeterminer)
        {
            this.pingDeterminer = pingDeterminer;
            this.matchClock = matchClock;
            this.server = server;
            round = 1;
            maxRounds = ServerConfig.GetInt("MaxRounds");
            matchGameEventWrapper.GameStarted += Initialize;
        }

        public void Initialize(ServerCore server)
        {
            playerToScore = new Dictionary<Server_ServerClient, PlayerScore>();
            scores = new List<PlayerScore>();
            for (int i = 0; i < server.clientManager.GetClients().Count; i++)
            {
                var score = new PlayerScore()
                {
                    guid = server.clientManager.GetClients()[i].info.GUID,
                    score = 0
                };
                playerToScore.Add(server.clientManager.GetClients()[i], score);
                scores.Add(score);
            }
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_RoundEnded);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            playerToScore[client].score++;
            if (round == maxRounds)
            {
                GameOver();
            }
            else
            {
                NewRound(client);
            }
        }

        private void NewRound(Server_ServerClient client)
        {
            round++;
            Message_ServerResponse_RoundEnded msg = new Message_ServerResponse_RoundEnded();
            msg.Winner = client.info.GUID;
            msg.Scores = scores;

            server.messageSender.SendToAll(msg, server.clientManager.GetClients());

            Message_ServerCommand_RoundReset resetMsg = new Message_ServerCommand_RoundReset()
            {
                timeRoundStart = GenerateTimeForNextMatch()
            };
            server.messageSender.SendToAll(resetMsg, server.clientManager.GetClients());
        }

        private double GenerateTimeForNextMatch()
        {
            return matchClock.GetTime() + pingDeterminer.GetMatchPingInMiliSeconds() * 2;
        }

        private void GameOver()
        {
            scores.Sort((x, y) => x.score.CompareTo(y.score));

            foreach (KeyValuePair<Server_ServerClient,PlayerScore> player in playerToScore)
            {
                if(player.Value.guid == scores[scores.Count - 1].guid)
                    SendResultMessage(player.Key, player.Value,true);
                else
                    SendResultMessage(player.Key, player.Value,false);
            }
        }

        private void SendResultMessage(Server_ServerClient client,PlayerScore playerScore,bool won)
        {
            Message_Update_MatchFinished msg = new Message_Update_MatchFinished(won);
            server.messageSender.Send(msg, client);
        }

    }
}