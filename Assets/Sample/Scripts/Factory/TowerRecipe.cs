using RockyECS;

namespace Sample
{
    public abstract class TowerRecipe : Recipe
    {
        public override void SetupUnit(Entity e)
        {
            e.AddComp<C_Position>();
            e.AddComp<C_Rotation>();
            SetupTower(e);
        }

        public abstract void SetupTower(Entity tower);
    }
}
