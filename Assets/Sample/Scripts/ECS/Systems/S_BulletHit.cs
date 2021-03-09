using System.Collections.Generic;
using System.Linq;
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
                    OnDamage(target, 1, hp, selection.container);
                }
                selection.container.Remove(s.id);
            }
        }

        private void OnDamage(Entity target, int damage, C_MonsterHp hp, Container container)
        {
            hp.rest -= damage;
            if (hp.rest == 0)
            {
                C_MonsterBonus bonus = target.GetComp<C_MonsterBonus>();
                int value = bonus != null ? bonus.value : 0;
                if (container.Remove(target.id))
                {
                    List<Entity> list = ListPool<Entity>.Get();
                    container.Find<C_IdentifyPlayer>(null, list, 1);
                    list.First().GetComp<C_PlayerProperties>().coins += value;
                    ListPool<Entity>.Recycle(list);
                }
            }
        }

    }
}
