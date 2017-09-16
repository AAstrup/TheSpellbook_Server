using System;
using System.Collections.Generic;

namespace Match
{
    public class GameEngine
    {
        /// <summary>
        /// WIP Sender must be replaced with interface allowing for bot games, and local test fo cards
        /// 
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="sender"></param>
        public GameEngine(MatchThread thread,Shared_PlayerInfo p1,Shared_PlayerInfo p2)
        {
            this.thread = thread;
            this.p1 = MakePlayer(p1);
            this.p2 = MakePlayer(p2);

            GUIDToPlayerClient = new Dictionary<int, Shared_InGame_PlayerInfo>();
            GUIDToPlayerClient.Add(p1.GUID, this.p1);
            GUIDToPlayerClient.Add(p2.GUID, this.p2);
        }

        private MatchThread thread;
        public Shared_InGame_PlayerInfo p1;
        public Shared_InGame_PlayerInfo p2;
        public Dictionary<int, Shared_InGame_PlayerInfo> GUIDToPlayerClient;

        private Shared_InGame_PlayerInfo MakePlayer(Shared_PlayerInfo Info)
        {
            Shared_InGame_PlayerInfo toReturn = new Shared_InGame_PlayerInfo();
            toReturn.cardsInDeck = new List<int>();
            toReturn.cardsInHand = new List<int>();
            toReturn.GUID = Info.GUID;
            toReturn.name = Info.name;
            toReturn.skillRaiting = Info.skillRaiting;
            return toReturn;
        }

        /// <summary>
        /// WIP
        /// Play a card with cardID
        /// </summary>
        /// <param name="cardID"></param>
        /// <param name="playerGUID"></param>
        public void PlayCard(int cardID, int playerGUID)
        {
            Win(playerGUID);
        }

        public void Win(int playerGUID)
        {
            foreach (KeyValuePair<int,Shared_InGame_PlayerInfo> item in GUIDToPlayerClient)
            {
                if (item.Key == playerGUID)
                {
                    Message_Update_MatchFinished winnerMsg = new Message_Update_MatchFinished(true);
                    thread.Send(item.Key, winnerMsg);
                    Console.WriteLine();
                }
                else
                {
                    Message_Update_MatchFinished loserMsg = new Message_Update_MatchFinished(false);
                    thread.Send(item.Key,loserMsg);
                }
            }
        }
    }
}