using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_Common_Transform
    {
        public float xPos;
        public float yPos;
        public float zPos;
        public float xRotation;
        public float yRotation;
        public float zRotation;
    }
}