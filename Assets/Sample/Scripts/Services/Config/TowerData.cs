using System;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "TowerDefence/DataStruts/TowerData")]
    public class TowerData :ScriptableObject
    {
        public TowerEntry[] m;
    }
}
