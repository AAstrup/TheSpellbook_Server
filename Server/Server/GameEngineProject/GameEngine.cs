using System;
using System.Collections.Generic;

public class GameEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">When using internet this class is responsible for sending messages if needed</param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    public GameEngine(IGameEngineSender sender,Shared_PlayerInfo p1,Shared_PlayerInfo p2)
    {
        this.sender = sender;
        this.p1 = MakePlayer(p1);
        this.p2 = MakePlayer(p2);
        turnManager = new TurnManager(p1,p2);
    }

    private IGameEngineSender sender;
    public Shared_InGame_PlayerInfo p1;
    public Shared_InGame_PlayerInfo p2;
    public TurnManager turnManager;
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
        sender.Win(playerGUID);
    }
}
