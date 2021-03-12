using RockyECS;

namespace Sample
{
    public class TowerRecipe : IRecipe
    {
        public void Setup(Entity e, Context context)
        {
            e.AddComp<C_Position>();
            e.AddComp<C_Rotation>();

            TowerEntry towerEntry = e.GetComp<C_TowerConfig>().cfg;

            C_Asset asset = e.AddComp<C_Asset>();
            asset.mesh = "Mesh/TowerTest"; // towerEntry.asset;
            asset.material = "Materials/Blue";

            e.AddComp<C_Renderer>();


            var findTarget = e.AddComp<C_TowerFindTarget>();
            findTarget.range = towerEntry.range / 100f;
            e.AddComp<C_TowerFindTargetGizmos>();


            C_TowerAttack attack = e.AddComp<C_TowerAttack>();
            attack.timer = 0;
            attack.preAttack = towerEntry.postAttack;
            attack.postAttack = towerEntry.postAttack;

            e.AddComp<C_Target>();
        }
    }
}
