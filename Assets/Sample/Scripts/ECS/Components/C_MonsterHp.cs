using AillieoUtils;
using RockyECS;
using System.Collections.Generic;
using System.Linq;

namespace Sample
{
    public class C_MonsterHp : IComponent
    {
        public int max = 3;
        public int rest = 3;

        public void OnDamage(Entity target, int damage)
        {
            rest -= damage;
            if (rest == 0)
            {
                C_MonsterBonus bonus = target.GetComp<C_MonsterBonus>();
                int value = bonus != null ? bonus.value : 0;
                if (RockyECS.Container.Instance.Remove(target.id))
                {
                    List<Entity> list = ListPool<Entity>.Get();
                    Container.Instance.Find<C_IdentifyPlayer>(null, list, 1);
                    list.First().GetComp<C_PlayerProperties>().coins += value;
                    ListPool<Entity>.Recycle(list);
                }
            }
        }

        public void Reset()
        {
        }
    }
}
