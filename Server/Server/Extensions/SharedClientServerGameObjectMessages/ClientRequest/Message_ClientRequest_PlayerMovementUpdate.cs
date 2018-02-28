using System;

[Serializable]
public class Message_ClientRequest_PlayerMovementUpdate
{
    public int GMJGUID;
    public float currentXPos;
    public float currentZPos;
    public float moveTargetXPos;
    public float moveTargetZPos;
    /// <summary>
    /// Time of the clock on which the spell were requested to be casted
    /// </summary>
    public double TimeStartedMoving;
}