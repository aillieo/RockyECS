using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public class C_MonsterFindPath : IComponent
    {
        public LinkedListNode<LevelEntry.TileData> target;
        public float rotating;

        public void Reset()
        {
            target = null;
        }
    }
}
