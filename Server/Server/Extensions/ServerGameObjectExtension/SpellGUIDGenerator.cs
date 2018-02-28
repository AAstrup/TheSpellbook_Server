namespace ServerGameObjectExtension
{
    public class SpellGUIDGenerator
    {
        private int SpellGUID;

        public SpellGUIDGenerator()
        {
            SpellGUID = 1;
        }


        public int GenerateGUID()
        {
            return SpellGUID++;
        }
    }
}