using RockyECS;

namespace Sample
{
    public abstract class SlotRecipe : Recipe
    {
        public override void SetupUnit(Entity e)
        {
            e.AddComp<C_Position>();
            e.AddComp<C_Rotation>();
            SetupSlot(e);
        }
        
        public abstract void SetupSlot(Entity slot);
    }
}
