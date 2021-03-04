using System;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "TowerEntry", menuName = "TowerDefence/DataStruts/TowerEntry")]
    public class TowerEntry : ScriptableObject, ICfgEntry
    {
        public int id;
        public int level;
        public int nextLevel;
        public int cost;
        public int sell;
        public int range;
        public int damage;
        public float preAttack;
        public float postAttack;
        public string icon;
        public string recipe;
        public string asset;

        public int key => id;
    }
}
