using System.Collections.Generic;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_BulletHit : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_BulletHitTargetAlways>() & new Filter<C_MoveEnd>() & new Filter<C_Target>();
        }

        public void Update(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                Entity target = s.GetComp<C_Target>().GetEntity();
                if (!Entity.IsNullOrInvalid(target))
                {
                    C_MonsterHp hp = target.GetComp<C_MonsterHp>();
                    //Debug.LogError($" 子弹{s.id} 击中 敌人{target.id}！");
                    hp.OnDamage(target, 1);
                }
                Container.Instance.Remove(s.id);
            }
        }

    }
}
