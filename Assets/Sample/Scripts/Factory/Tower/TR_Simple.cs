using RockyECS;

namespace Sample
{
    public class TR_Simple : TowerRecipe
    {
        public override void SetupTower(Entity tower)
        {
            TowerEntry towerEntry = tower.GetComp<C_TowerConfig>().cfg;

            C_Asset asset = tower.AddComp<C_Asset>();
            asset.mesh = "Mesh/TowerTest"; // towerEntry.asset;
            asset.material = "Materials/Blue";

            tower.AddComp<C_Renderer>();


            var findTarget = tower.AddComp<C_TowerFindTarget>();
            findTarget.range = towerEntry.range / 100f;
            tower.AddComp<C_TowerFindTargetGizmos>();


            C_TowerAttack attack = tower.AddComp<C_TowerAttack>();
            attack.timer = 0;
            attack.preAttack = towerEntry.postAttack;
            attack.postAttack = towerEntry.postAttack;

            tower.AddComp<C_Target>();
        }
    }

}


