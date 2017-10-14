using System;

[Serializable]
public class Message_ClientCommand_PlayerMovementUpdate
{
    public int GMJGUID;
    public float currentXPos;
    public float currentZPos;
    public float moveTargetXPos;
    public float moveTargetZPos;
}