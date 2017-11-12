using System;

namespace ServerGameObjectExtension
{
    public struct VectorXZ
    {
        private float x;
        private float z;

        public VectorXZ(float x, float z)
        {
            this.x = x;
            this.z = z;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(z, 2));
        }

        public void Normalise (VectorXZ vector, float amount){

            }
    }
}