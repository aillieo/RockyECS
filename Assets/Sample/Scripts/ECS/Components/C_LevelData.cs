using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public class C_LevelData : IComponent
    {
        public int loadingPercent;
        public bool isDone;
        public LevelEntry.MapData mapData;
        public LevelEntry level;

        public LinkedList<LevelEntry.TileData> paths = new LinkedList<LevelEntry.TileData>();
        public List<LevelEntry.Wave> monsterSequences = new List<LevelEntry.Wave>();

        public void Reset()
        {
            isDone = false;
            loadingPercent = 0;
            mapData = null;
            level = null;
            paths.Clear();
            monsterSequences.Clear();
        }

    }
}
