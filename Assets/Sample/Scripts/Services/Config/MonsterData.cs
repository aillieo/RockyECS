using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "TowerDefence/DataStruts/MonsterData")]
    public class MonsterData : ScriptableObject
    {
        public MonsterEntry[] m;
    }
}
