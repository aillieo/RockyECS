using System;
using AillieoUtils;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "TowerDefence/DataStruts/LevelData")]
    public class LevelData : ScriptableObject
    {
        public LevelEntry[] m;
    }
}
