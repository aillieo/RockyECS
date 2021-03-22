using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public class C_LevelLoad : IComponent
    {
        public int loadingPercent;
        public bool isDone;
        public LevelEntry.MapData mapData;
        public LevelEntry level;

        public void Reset()
        {
            isDone = false;
            loadingPercent = 0;
            mapData = null;
            level = null;
        }

    }
}
