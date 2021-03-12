using RockyECS;

namespace Sample
{
    public class SlotRecipe : IRecipe
    {
        public void Setup(Entity e, Context context)
        {
            e.AddComp<C_Position>();
            e.AddComp<C_Rotation>();

            C_Collider collider = e.AddComp<C_Collider>();
            collider.threshold = 0.15f;

            e.AddComp<C_ClickToBuild>();

            e.AddComp<C_Renderer>();

            C_Asset asset = e.AddComp<C_Asset>();
            asset.mesh = "Mesh/TowerSlot";
            asset.material = "Materials/Default";
        }
    }
}
