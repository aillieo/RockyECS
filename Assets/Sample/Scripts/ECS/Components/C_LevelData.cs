using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public class C_LevelData : IComponent
    {
        public LinkedList<LevelEntry.TileData> paths = new LinkedList<LevelEntry.TileData>();
        public List<LevelEntry.Wave> monsterSequences = new List<LevelEntry.Wave>();

        public void Reset()
        {
            paths.Clear();
            monsterSequences.Clear();
        }

    }
}
