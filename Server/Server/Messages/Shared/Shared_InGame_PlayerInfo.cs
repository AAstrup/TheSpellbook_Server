using System;
using System.Collections.Generic;

namespace Match
{
    [Serializable]
    public class Shared_InGame_PlayerInfo
    {
        public string name;
        public int GUID;
        public List<int> cardsInHand;
        public List<int> cardsInDeck;
        public int skillRaiting;

        public Shared_InGame_PlayerInfo GetAsHidden()
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                cardsInHand[i] = -1;
            }
            return this;
        }
    }
}