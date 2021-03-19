using RockyECS;

namespace Sample
{
    public class S_TowerAttack : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_TowerAttack>();
        }

        public void Update(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                C_TowerAttack c = s.GetComp<C_TowerAttack>();

                Entity target = s.GetComp<C_Target>().TargetAsEntity(selection.context);

                //UnityEngine.Debug.LogError("Entity.IsNullOrInvalid(target) =" + Entity.IsNullOrInvalid(target));
                if (!Entity.IsNullOrInvalid(target))
                {
                    c.timer += deltaTime;

                    if (c.timer > c.preAttack)
                    {
                        c.timer -= (c.preAttack + c.postAttack);
                        Entity bullet = selection.context.Create(c.bulletRecipe);
                        bullet.SetPosition(s.GetPosition());
                        bullet.GetComp<C_TargetPos>().targetPos = target.GetPosition();
                        bullet.GetComp<C_Target>().target = s.GetComp<C_Target>().target;
                    }
                }
                else
                {
                    c.timer = 0;
                }
            }
        }
    }
}
