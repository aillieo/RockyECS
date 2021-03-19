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
                    C_PlayerProperties p = selection.context.SelectOne<C_PlayerProperties>().GetComp<C_PlayerProperties>();
                    OnArrive(p);
                    selection.context.Remove(s.id);
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

        private void OnArrive(C_PlayerProperties p)
        {
            p.hpRest -= 1;
            p.hpRest = Mathf.Max(p.hpRest, 0);
            Debug.LogError($"c.hpRest = {p.hpRest}");
        }
    }
}
