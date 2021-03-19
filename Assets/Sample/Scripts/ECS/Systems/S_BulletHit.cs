using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_BulletHit : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter[] CreateFilters()
        {
            return new []
            {
                new Filter<C_BulletHitTargetAlways>() & new Filter<C_MoveEnd>() & new Filter<C_Target>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                Entity target = s.GetComp<C_Target>().TargetAsEntity(selection.context);
                if (!Entity.IsNullOrInvalid(target))
                {
                    C_MonsterHp hp = target.GetComp<C_MonsterHp>();
                    //Debug.LogError($" 子弹{s.id} 击中 敌人{target.id}！");
                    OnDamage(target, 1, hp, selection.context);
                }
                selection.context.Remove(s.id);
            }
        }

        private void OnDamage(Entity target, int damage, C_MonsterHp hp, Context context)
        {
            hp.rest -= damage;
            if (hp.rest == 0)
            {
                C_MonsterBonus bonus = target.GetComp<C_MonsterBonus>();
                int value = bonus != null ? bonus.value : 0;
                if (context.Remove(target.id))
                {
                    List<Entity> list = ListPool<Entity>.Get();
                    context.Find<C_IdentifyPlayer>(null, list, 1);
                    list.First().GetComp<C_PlayerProperties>().coins += value;
                    ListPool<Entity>.Recycle(list);
                }
            }
        }

    }
}
