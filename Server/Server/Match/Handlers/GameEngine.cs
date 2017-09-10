using System;
using System.Collections.Generic;

namespace Match
{
    public class GameEngine
    {
        public GameEngine(Shared_PlayerInfo p1,Shared_PlayerInfo p2)
        {
            this.p1 = MakePlayer(p1);
            this.p2 = MakePlayer(p2);
        }

        public Shared_InGame_PlayerInfo p1;
        public Shared_InGame_PlayerInfo p2;
        public Shared_InGame_PlayerInfo GetP2()
        {
            return p2;
        }
        public Shared_InGame_PlayerInfo GetP1()
        {
            return p1;
        }

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
    }
}