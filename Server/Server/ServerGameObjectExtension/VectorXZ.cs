using System;

namespace ServerGameObjectExtension
{
    public struct VectorXZ
    {
        public float x;
        public float z;

        public VectorXZ(float x, float z)
        {
            this.x = x;
            this.z = z;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(z, 2));
        }

        public void Normalise (){
            float magnitude = Magnitude();
            x = x / magnitude;
            z = z / magnitude;
        }

        public void Multiply(float amount)
        {
            x = x * amount;
            z = z * amount;
        }
    }
}