using AillieoUtils;
using System;
using System.Collections.Generic;
using AillieoUtils.TypeExt;

namespace Sample
{
    public class CfgProxy: Singleton<CfgProxy>
    {
        public CfgProxy()
        {
            var sd = Load<LevelData>("LevelData");
            Dictionary<int, ICfgEntry> dsd = data.GetOrAdd(typeof(LevelEntry), o => new Dictionary<int, ICfgEntry>());
            sd.m.ForEach(p => dsd.Add(p.id, p));

            var md = Load<MonsterData>("MonsterData");
            Dictionary<int, ICfgEntry> dmd = data.GetOrAdd(typeof(MonsterEntry), o => new Dictionary<int, ICfgEntry>());
            md.m.ForEach(p => dmd.Add(p.id, p));

            var td =Load<TowerData>("TowerData");
            Dictionary<int, ICfgEntry> dtd = data.GetOrAdd(typeof(TowerEntry), o => new Dictionary<int, ICfgEntry>());
            td.m.ForEach(p => dtd.Add(p.id, p));
        }

        private Dictionary<Type, Dictionary<int, ICfgEntry>> data = new Dictionary<Type, Dictionary<int, ICfgEntry>>();

        private T Load<T>(string assetName) where T: UnityEngine.Object
        {
            var rawData = ResourceManager.Instance.LoadAsset<T>("Config/" + assetName);
            return rawData;
        }

        public T Get<T>(int id) where T : class, ICfgEntry
        {
            var d1 = data.GetOrDefault(typeof(T));
            return d1.GetOrDefault(id) as T;
        }
    }
}
