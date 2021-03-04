using System.Collections.Generic;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_MonsterFindPath : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_MonsterFindPath>() & (new Filter<C_MoveStart>() | new Filter<C_MoveEnd>());
        }

        public void Update(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                C_MonsterFindPath c = s.GetComp<C_MonsterFindPath>();


                if (c.target == null)
                {
                    return;
                }

                if (c.target.Next == null)
                {
                    OnArrive();
                    Container.Instance.Remove(s.id);
                    return;
                }


                c.target = c.target.Next;
                s.SetRotation((c.target.Value.position - s.GetPosition()).ToRotation());
                s.GetComp<C_TargetPos>().targetPos = c.target.Value.position;

                if(s.HasComp<C_MoveStart>())
                {
                    s.RemoveComp(s.GetComp<C_MoveStart>());
                }
                if (s.HasComp<C_MoveEnd>())
                {
                    s.RemoveComp(s.GetComp<C_MoveEnd>());
                }
                //s.AddComp<C_MoveToPos>();
            }
        }

        private void OnArrive()
        {
            List<Entity> list = ListPool<Entity>.Get();
            Container.Instance.Find<C_IdentifyPlayer>(null, list, 1);
            Entity player = list[0];
            ListPool<Entity>.Recycle(list);
            C_PlayerProperties c = player.GetComp<C_PlayerProperties>();
            c.hpRest -= 1;
            c.hpRest = Mathf.Max(c.hpRest, 0);
            Debug.LogError($"c.hpRest = {c.hpRest}");
        }
    }
}
