using System;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "LevelEntry", menuName = "TowerDefence/DataStruts/LevelEntry")]
    public class LevelEntry : ScriptableObject, ICfgEntry
    {
        [Serializable]
        public class Wave
        {
            public float delay;
            public int monsterID;
            public int count;
            public int interval;
        }

        [Serializable]
        public class WaveInfo
        {
            [SerializeField]
            public Wave[] waves;
        }

        public int id;
        public string levelName;
        public string mapDataPath;
        public string mapEntityPath;
        [SerializeField]
        public WaveInfo waveInfo;

        public MapData mapData;

        public int key => id;


        [Serializable]
        public class TileData
        {
            public int id;          // 静态id 可以和 pos 转换 不同地图尺寸转换公式不同

            public Vector2 position;
            public GameDefine.TileType type = GameDefine.TileType.Empty;
        }

        [Serializable]
        public class MapData
        {
            public string mapName;
            public int width;
            public int height;

            public TileData[] pathTiles;
            public TileData[] functionTiles;
        }
    }
}
