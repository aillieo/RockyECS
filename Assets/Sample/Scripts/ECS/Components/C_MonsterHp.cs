using AillieoUtils;
using RockyECS;
using System.Collections.Generic;
using System.Linq;

namespace Sample
{
    public class C_MonsterHp : IComponent
    {
        public int max = 3;
        public int rest = 3;

        public void Reset()
        {
        }
    }
}
