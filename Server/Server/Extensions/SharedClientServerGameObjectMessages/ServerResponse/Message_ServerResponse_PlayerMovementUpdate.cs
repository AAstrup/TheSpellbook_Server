using System;

[Serializable]
public class Message_ServerResponse_PlayerMovementUpdate
{
    public int GMJGUID;
    public float currentXPos;
    public float currentZPos;
    public float moveTargetXPos;
    public float moveTargetZPos;
    /// <summary>
    /// Time of the clock on which the spell were considered to be casted, taken the match delay into account
    /// </summary>
    public double TimeStartedMovingWithPing;
}