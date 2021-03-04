using RockyECS;

namespace Sample
{
    public class C_PlayerProperties : IComponent
    {
        public int hpMax = 10;
        public int hpRest = 10;
        public int coins = 100;
        public void Reset()
        {
        }
    }
}
