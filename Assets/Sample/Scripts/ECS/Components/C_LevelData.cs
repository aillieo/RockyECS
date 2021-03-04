using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public class C_LevelData : IComponent
    {
        public int loadingFlag;
        public LevelEntry.MapData mapData;
        public LevelEntry level;

        public LinkedList<LevelEntry.TileData> paths = new LinkedList<LevelEntry.TileData>();
        public List<LevelEntry.Wave> monsterSequences = new List<LevelEntry.Wave>();

        public void Reset()
        {
            loadingFlag = 0;
            mapData = null;
            level = null;
            paths.Clear();
            monsterSequences.Clear();
        }

    }
}
