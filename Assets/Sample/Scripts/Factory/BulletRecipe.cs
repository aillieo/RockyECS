using RockyECS;

namespace Sample
{
    public abstract class BulletRecipe : Recipe
    {
        public override void SetupUnit(Entity e)
        {
            e.AddComp<C_Position>();
            e.AddComp<C_Rotation>();
            SetupBullet(e);
        }

        public abstract void SetupBullet(Entity bullet);
    }
}
