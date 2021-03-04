using System;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "MonsterEntry", menuName = "TowerDefence/DataStruts/MonsterEntry")]
    public class MonsterEntry : ScriptableObject, ICfgEntry
    {
        public int id;
        public int hp;
        public int speed;
        public int defence;
        public string recipe;
        public string asset;
        public int bonus;

        public int key => id;
    }
}
