using System.Collections.Generic;
using AillieoUtils;
using RockyECS;

namespace Sample
{
    public class S_TowerFindTarget : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_TowerFindTarget>();
        }

        public void Update(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                C_TowerFindTarget c = s.GetComp<C_TowerFindTarget>();

                C_Target cTarget = s.GetComp<C_Target>();

                if (Entity.IsNullOrInvalid(cTarget.GetEntity()))
                {
                    using (var scope = ListPool<Entity>.GetAutoRecycleScope())
                    {
                        List<Entity> list = scope.Get();
                        selection.container.Find<C_IdentifyMonster>(e =>
                        {
                            if ((e.GetPosition() - s.GetPosition()).sqrMagnitude <= c.range * c.range)
                            {
                                return true;
                            }

                            return false;
                        }, list, 1);

                        if (list.Count > 0)
                        {
                            cTarget.target = list[0].id;
                        }
                    }
                }
                else
                {
                    if ((cTarget.GetEntity().GetPosition() - s.GetPosition()).sqrMagnitude > c.range * c.range)
                    {
                        cTarget.target = Entity.invalid;
                    }
                    else
                    {
                        s.SetRotation((cTarget.GetEntity().GetPosition() - s.GetPosition()).ToRotation());
                    }
                }


            }
        }
    }

}
