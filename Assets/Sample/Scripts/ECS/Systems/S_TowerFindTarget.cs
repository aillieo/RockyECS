using System.Collections.Generic;
using AillieoUtils;
using RockyECS;

namespace Sample
{
    public class S_TowerFindTarget : BaseSystem, IFilteredUpdatingSystem
    {

        private IEnumerable<Entity> monsters;

        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_IdentifyMonster>(),
                new Filter<C_TowerFindTarget>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
            switch (filterIndex)
            {
                case 0:
                    monsters = selection;
                    break;
                case 1:
                    foreach (var s in selection)
                    {
                        C_TowerFindTarget c = s.GetComp<C_TowerFindTarget>();
                        C_Target cTarget = s.GetComp<C_Target>();

                        if (Entity.IsNullOrInvalid(cTarget.TargetAsEntity(selection.context)))
                        {
                            foreach (var m in monsters)
                            {
                                if ((m.GetPosition() - s.GetPosition()).sqrMagnitude <= c.range * c.range)
                                {
                                    cTarget.target = m.id;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if ((cTarget.TargetAsEntity(selection.context).GetPosition() - s.GetPosition()).sqrMagnitude > c.range * c.range)
                            {
                                cTarget.target = Entity.invalid;
                            }
                            else
                            {
                                s.SetRotation((cTarget.TargetAsEntity(selection.context).GetPosition() - s.GetPosition()).ToRotation());
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
